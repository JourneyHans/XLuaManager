using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using XLua;

public class XLuaManager : MonoBehaviour
{
    string luaScriptsPath = "/LuaScripts/";

    LuaEnv luaEnv;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        luaEnv = new LuaEnv();
//        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'LuaScripts/LuaEntry'");

        // HotFix
        luaEnv.DoString("require 'LuaScripts/HotFix/LuaHotFixEntry'");
    }

    /// <summary>
    /// 自定义Load
    /// </summary>
    public byte[] MyLoader(ref string fileName)
    {
        string absPath = PathUtils.STREAMING_ASSET_PATH + luaScriptsPath + fileName + ".lua";
        print("MyLoader: " + absPath);

        WWW wwwPath = new WWW(absPath);
        return wwwPath.bytes;

//        using (WWW wwwPath = new WWW(absPath))
//        {
//            yield return wwwPath;
//            return wwwPath.bytes;
//        }
    }

    IEnumerator WWWLoad(string absPath)
    {
        using (WWW wwwPath = new WWW(absPath))
        {
            yield return wwwPath;
            byte[] bytes = wwwPath.bytes;
        }
    }

    void OnDestroy()
    {
        luaEnv.Dispose();
    }
}
