using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum HotFixEnum
{
    Test = 1,
}

public class HotfixPanel : UIBase
{
    private Button SimpleTestBtn;
    private Button HotfixExTestBtn;
    private Button EnumFixTestBtn;

    void Awake()
    {
        SimpleTestBtn = transform.Find("Panel/SimpleTest").GetComponent<Button>();
        SimpleTestBtn.onClick.AddListener(SimpleBtnCall);

        HotfixExTestBtn = transform.Find("Panel/HotfixExTest").GetComponent<Button>();
        HotfixExTestBtn.onClick.AddListener(HotfixExBtnCall);

        EnumFixTestBtn = transform.Find("Panel/EnumFixTest").GetComponent<Button>();
        EnumFixTestBtn.onClick.AddListener(EnumFixTestBtnCall);
    }

    // 普通修复测试，这里会执行lua中修改后的代码，代替掉现有代码
    private void SimpleBtnCall()
    {
        Debug.Log("C# call SimpleBtnCall");
    }

    // 增补功能测试，这里会先执行C#原有代码，再执行Lua中的代码（实际上也能反过来）
    private void HotfixExBtnCall()
    {
        Debug.Log("First, C# call HotfixExBtnCall, then...");
    }

    // 测试枚举的一些用法，TODO：目前是有问题的，Lua端始终获取不到名字和值
    private void EnumFixTestBtnCall()
    {
        Debug.Log("C# -- Enum Value: " + (int)HotFixEnum.Test);
        Debug.Log("C# -- Enum Value To String: " + HotFixEnum.Test.ToString());
    }
}
