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

    private bool localMode = false;

    private AssetBundleManifest hotFixManifest;
    private Dictionary<string, AssetBundle> bundleMap = new Dictionary<string, AssetBundle>();

    private StringBuilder sb = new StringBuilder();

    public void Init()
    {
//        lua_ab_path = Application.persistentDataPath + "/LuaHotfix/";     // 正确热更新模式下读这个路径
        lua_ab_path = Application.streamingAssetsPath.Replace("/Assets", "") + "/LuaHotfix/";    // 本地模式

        luaEnv = new LuaEnv();
        if (localMode)
        {
            Debug.Log("XLua 本地模式");
            luaEnv.AddLoader(LoaderByLocal);
        }
        else
        {
            Debug.Log("XLua AB模式");

            HandleAB();

            luaEnv.AddLoader(LoaderByAB);
        }
        luaEnv.DoString("require 'main'");
    }

    // 处理AssetBundle
    private void HandleAB()
    {
        AssetBundle hotFixAB = AssetBundle.LoadFromFile(lua_ab_path + "LuaHotfix");
        hotFixManifest = hotFixAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        hotFixAB.Unload(false);

        foreach (string luaBundleName in hotFixManifest.GetAllAssetBundles())
        {
            Debug.Log("luaBundleName: " + luaBundleName);
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
        string fullPath = Application.dataPath + "/../LuaLogic/" + fileName + ".lua";
        return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
    }

    public void Destroy()
    {
        luaEnv.Dispose();
    }
}
