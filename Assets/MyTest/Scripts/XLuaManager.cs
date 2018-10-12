using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class XLuaManager : Singleton<XLuaManager>
{
    private LuaEnv luaEnv;
    private string luaPath = "/../LuaLogic";     // lua源文件
    private string lua_ab_path;

    private bool localMode = false;

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

            // 解压打包后的文件
            string zipFilePath = lua_ab_path.Remove(lua_ab_path.Length - 1);
            ZipUtil.Unzip(zipFilePath + ".zip", lua_ab_path);

            luaEnv.AddLoader(LoaderByAB);
        }
        luaEnv.DoString("require 'main'");
    }

    // AB模式下的loader，使用打包成AB的lua文件
    public byte[] LoaderByAB(ref string fileName)
    {
        int fileStartIdx = fileName.LastIndexOf('/') + 1;
        if (fileStartIdx != 0)
        {
            fileName = fileName.Remove(0, fileStartIdx);
        }
        Debug.Log("LoaderByAB: " + fileName);

        string fullPath = lua_ab_path + fileName + ".lua.bytes";
        byte[] bytes = File.ReadAllBytes(fullPath);
        AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
        TextAsset luaFile = bundle.LoadAsset<TextAsset>(fileName + ".lua");
        return luaFile.bytes;
    }

    // 本地模式的loader，使用lua源文件
    public byte[] LoaderByLocal(ref string fileName)
    {
        string fullPath = Application.dataPath + luaPath + "/" + fileName + ".lua";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
    }

    public void Destroy()
    {
        luaEnv.Dispose();
    }
}
