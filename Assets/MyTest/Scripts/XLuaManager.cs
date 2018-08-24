using System.Collections;
using System.IO;
using UnityEngine;
using XLua;

public class XLuaManager : Singleton<XLuaManager>
{
    private string luaScriptsPath = "/data/LuaHotfix";
    private LuaEnv luaEnv;

    private string luafileUrl = Application.streamingAssetsPath + "/LuaHotfix";     // 模拟存放lua文件的资源服务器地址

    private string[] fileNameList =
    {
        "main.lua",
    };

    // 初始化
    public IEnumerator Init()
    {
        // 清理LuaHotfix文件夹
        CleanFolder();

        // 更新
        yield return UpdateLuaFile();
    }

    /// <summary>
    /// 清理文件夹
    /// </summary>
    private void CleanFolder()
    {
        if (Directory.Exists(Application.persistentDataPath + luaScriptsPath))
        {
            Directory.Delete(Application.persistentDataPath + luaScriptsPath, true);
        }
    }

    /// <summary>
    /// 更新Lua代码
    /// </summary>
    private IEnumerator UpdateLuaFile()
    {
        // 下载文件
        foreach (var fileName in fileNameList)
        {
            yield return DownloadLuaScript(luafileUrl, fileName);
        }

        // 开始lua
        Start();
    }

    private IEnumerator DownloadLuaScript(string url, string fileName)
    {
        WWW www = new WWW(url + fileName);
        if (www.error != null)
        {
            Debug.Log("Download Error: " + www.error);
        }

        while (!www.isDone)
        {
            yield return null;
            // TODO:这个地方可以放进度相关
        }

        if (www.isDone)
        {
            IOManager.SaveByte(PathManager.GetPersistentIOPath() + luaScriptsPath + "/" + fileName, www.bytes);
        }
    }

    /// <summary>
    /// lua入口
    /// </summary>
    public void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'main'");
    }

    /// <summary>
    /// 自定义Load
    /// </summary>
    public byte[] MyLoader(ref string fileName)
    {
        string fullPath = Application.persistentDataPath + luaScriptsPath + "/" + fileName + ".lua";
        Debug.Log("MyLoader: " + fullPath);
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
    }
}
