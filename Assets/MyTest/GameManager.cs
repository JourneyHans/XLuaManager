using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class GameManager : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        XLuaManager.Instance.Init();
    }
}
