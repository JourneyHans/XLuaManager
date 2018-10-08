using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class XLuaManager : Singleton<XLuaManager>
{
    private LuaEnv luaEnv;
    private string lua_ab_path;

    public void Init()
    {
//        lua_ab_path = Application.persistentDataPath + "/bundle/xlua/out/";     // 正确热更新模式下读这个路径
        lua_ab_path = Application.streamingAssetsPath.Replace("/Assets", "") + "/LuaHotfix/out/";    // 本地模式
        Debug.Log(lua_ab_path);

        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'main'");
    }

    public byte[] MyLoader(ref string fileName)
    {
        string fullPath = lua_ab_path + fileName + ".lua.bytes";
        byte[] bytes = File.ReadAllBytes(fullPath);
        AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
        TextAsset luaFile = bundle.LoadAsset<TextAsset>(fileName + ".lua");
        return luaFile.bytes;
    }

    void OnDestroy()
    {
        luaEnv.Dispose();
    }
}
