using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class XLuaManager : Singleton<XLuaManager>
{
    private LuaEnv luaEnv;
    private string lua_ab_path;

    private bool localMode = GameManager.Instance.XLuaLocalMode;

    private AssetBundleManifest hotFixManifest;
    private Dictionary<string, AssetBundle> bundleMap = new Dictionary<string, AssetBundle>();
    private StringBuilder sb = new StringBuilder();

    /********* 热更模式相关 *********/
    // 文件名和对应的MD5码
    private Dictionary<string, string> fileMap = new Dictionary<string, string>();
    private string filesTxtPath = PathUtil.persistentDataPath + "LuaHotfix/files.txt";

    public void Init()
    {
        if (localMode)
        {
            SetUpLuaEnv();
        }
        else
        {
            CheckLuaFiles();
        }
    }

    // 检测热更files.txt文件
    private void CheckLuaFiles()
    {
        // 下载最新的files文件
        string filesUrl = "ftp://10.128.2.223/LuaHotfix/files.txt";
        DownloadManager.Instance.Download(filesUrl, delegate(WWW www)
        {
            if (www == null)
            {
                return;
            }
            // 保存files文件
            IOManager.SaveFile(filesTxtPath, www.bytes);

            // 处理files文件中的数据
            HandleLuaVersionFiles();

            // 更新Lua代码相关Bundle
            UpdateLuaBundles();
        });
    }

    // 处理lua版本文件
    private void HandleLuaVersionFiles()
    {
        string[] content = File.ReadAllLines(filesTxtPath, Encoding.UTF8);

        foreach (string line in content)
        {
            string[] split = line.Split('|');
            fileMap.Add(split[0], split[1]);
        }
    }

    // 更新Lua代码相关Bundle
    private void UpdateLuaBundles()
    {
        string urlRoot = "ftp://10.128.2.223/LuaHotfix/";
        string rootPath = PathUtil.persistentDataPath + "LuaHotfix/";

        int index = 0;
        int fileCount = fileMap.Count;
        float progress = 0;

        foreach (KeyValuePair<string, string> filePair in fileMap)
        {
            string fileName = filePair.Key;         // 文件名
            string fileMd5 = filePair.Value;        // 文件对应的MD5
            string fileUrl = urlRoot + fileName;    // 文件在服务器上的地址
            string filePath = rootPath + fileName;  // 文件保存的位置

            // 文件不存在或者文件md5码对应不上，则需要更新
            bool isCanUpdate = !File.Exists(filePath) ||
                                Util.md5file(filePath) != fileMd5;
            if (isCanUpdate)
            {
                DownloadManager.Instance.Download(fileUrl, delegate (WWW www)
                {
                    // 保存文件
                    IOManager.SaveFile(filePath, www.bytes);
                    index++;
                    progress = (float)index / fileCount;
                    Debug.LogFormat("Update new file:{0}\t\tCurrent progress: {1}%", fileName, progress * 100);
                    if (index == fileCount)
                    {
                        SetUpLuaEnv();
                    }
                });
            }
            else
            {
                index++;
                progress = (float)index / fileCount;
                Debug.LogFormat("Skip file:{0}\t\tCurrent progress: {1}%", fileName, progress * 100);
                if (index == fileCount)
                {
                    SetUpLuaEnv();
                }
            }

            Messenger<float>.Invoke("UpdateResource", progress);
        }
    }

    // 设置lua环境
    private void SetUpLuaEnv()
    {
        luaEnv = new LuaEnv();
        if (localMode)
        {
            Debug.Log("XLua 本地模式");
            luaEnv.AddLoader(LoaderByLocal);
            Messenger<float>.Invoke("UpdateResource", 1f);
        }
        else
        {
            Debug.Log("XLua AB模式");

            // 处理AssetBundle
            HandleAB();

            luaEnv.AddLoader(LoaderByAB);
        }
        luaEnv.DoString("require 'main'");
    }

    // 处理AssetBundle
    private void HandleAB()
    {
        // 正确热更新模式下读这个路径
        lua_ab_path = PathUtil.persistentDataPath + "LuaHotfix/";

//        lua_ab_path = Application.streamingAssetsPath.Replace("/Assets", "") + "/LuaHotfix/";
        AssetBundle hotFixAB = AssetBundle.LoadFromFile(lua_ab_path + "LuaHotfix");
        hotFixManifest = hotFixAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        hotFixAB.Unload(false);

        foreach (string luaBundleName in hotFixManifest.GetAllAssetBundles())
        {
//            Debug.Log("luaBundleName: " + luaBundleName);
            AssetBundle bundle = AssetBundle.LoadFromFile(lua_ab_path + luaBundleName);
            if (bundle != null)
            {
                string bundleName = luaBundleName.Replace("lua/", string.Empty).Replace(".unity3d", "");
                bundleMap[bundleName] = bundle;
            }
        }
    }

    // AB模式下的loader，使用打包成AB的lua文件
    private byte[] LoaderByAB(ref string fileName)
    {
        byte[] buffer = null;
        sb.Append("lua");
        int pos = fileName.LastIndexOf('/');
        if (pos > 0)
        {
            sb.Append("_");
            sb.Append(fileName, 0, pos).Replace('/', '_');
            fileName = fileName.Substring(pos + 1);
        }

        if (!fileName.EndsWith(".lua"))
        {
            fileName += ".lua";
        }

        fileName += ".bytes";
        AssetBundle bundle;
        bundleMap.TryGetValue(sb.ToString(), out bundle);
        if (bundle != null)
        {
            TextAsset luaCode = bundle.LoadAsset<TextAsset>(fileName);
            if (luaCode != null)
            {
                buffer = luaCode.bytes;
                Resources.UnloadAsset(luaCode);
            }
        }

        sb.Length = 0;
        sb.Capacity = 0;

        return buffer;
    }

    // 本地模式的loader，使用lua源文件
    private byte[] LoaderByLocal(ref string fileName)
    {
        // 直接读lua源文件
        string fullPath = Application.dataPath + "/LuaLogic/" + fileName + ".lua";
        return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
    }

    public void Destroy()
    {
        luaEnv.Dispose();
    }
}
