using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour
{
    public Text title;
    private string key;

	void Start ()
	{
	    key = EncryptKey.desKey;
	    Show();
	}

    void Show()
    {
        title.text = key;
    }
}

/// <summary>
/// 临时的key
/// </summary>
public struct EncryptKey
{
    public static string desKey = "1111111";   // 64位
}
