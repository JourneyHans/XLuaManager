using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class XLuaManager : Singleton<XLuaManager>
{
    LuaEnv luaEnv;

    public void Init()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'main'");
    }

    public byte[] MyLoader(ref string fileName)
    {
        //string lua_ab_path = Application.persistentDataPath + "/bundle/xlua/out/";
        string lua_ab_path = Application.streamingAssetsPath + "/bundle/xlua/out/";
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
