using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using XLua;

public class GameManager : SingletonUnity<GameManager>
{
    // Use this for initialization
    void Awake()
    {
        Init();
    }

    void Start()
    {
        // XLua初始化
        XLuaManager.Instance.Init();
    }

    // 初始化
    private void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
