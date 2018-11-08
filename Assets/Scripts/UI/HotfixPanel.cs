using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotfixPanel : UIBase
{
    private Button SimpleTestBtn;
    private Button HotfixExTestBtn;

    protected override void Awake()
    {
        base.Awake();
        SimpleTestBtn = transform.Find("Panel/SimpleTest").GetComponent<Button>();
        SimpleTestBtn.onClick.AddListener(SimpleBtnCall);
        HotfixExTestBtn = transform.Find("Panel/HotfixExTest").GetComponent<Button>();
        HotfixExTestBtn.onClick.AddListener(HotfixExBtnCall);
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
}
