using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI基类
public class UIBase : MonoBehaviour
{
    public string UIName { set; get; }

    protected virtual void Awake()
    {

    }

	protected virtual void Start ()
	{
		
	}

    protected virtual void Update ()
    {
		
	}

    // 关闭方法
    protected virtual void Close()
    {
        UIManager.Instance.Close(UIName);
    }
}
