using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonUnity<GameManager>
{
    public bool LoadFromUrl = false;

    void Awake()
    {
        
    }

    void Start()
    {
        // 开始处理资源
        StartCoroutine(ProcessRes());
    }

    private IEnumerator ProcessRes()
    {
        yield return XLuaManager.Instance.Init();
    }
}
