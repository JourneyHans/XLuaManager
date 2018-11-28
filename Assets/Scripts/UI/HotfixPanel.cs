using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotfixPanel : UIBase
{
    public enum HotFixEnum
    {
        Test = 1,
    }

    private Button BackBtn;
    private Button SimpleTestBtn;
    private Button HotfixExTestBtn;
    private Button EnumFixTestBtn;
    private Button CollectionFixTestBtn;

    void Awake()
    {
        BackBtn = transform.Find("Back").GetComponent<Button>();
        BackBtn.onClick.AddListener(Close);

        SimpleTestBtn = transform.Find("Panel/SimpleTest").GetComponent<Button>();
        SimpleTestBtn.onClick.AddListener(SimpleBtnCall);

        HotfixExTestBtn = transform.Find("Panel/HotfixExTest").GetComponent<Button>();
        HotfixExTestBtn.onClick.AddListener(HotfixExBtnCall);

        EnumFixTestBtn = transform.Find("Panel/EnumFixTest").GetComponent<Button>();
        EnumFixTestBtn.onClick.AddListener(EnumFixTestBtnCall);

        CollectionFixTestBtn = transform.Find("Panel/CollectionFixTest").GetComponent<Button>();
        CollectionFixTestBtn.onClick.AddListener(CollectionFixBtnCall);
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

    // 测试枚举的一些用法
    private void EnumFixTestBtnCall()
    {
        Debug.Log("C# -- Enum Value: " + (int)HotFixEnum.Test);
        Debug.Log("C# -- Enum Value To String: " + HotFixEnum.Test.ToString());
    }

    // 容器修复测试
    public List<int> intList = new List<int> {1, 2, 3, 4};
    public Dictionary<int, string> dic = new Dictionary<int, string>
    {
        {1, "a"}, {2, "b"}, {3, "c"}, {4, "d"},
    };
    public Dictionary<string, int> dic_1 = new Dictionary<string, int>
    {
        {"a", 1}, {"b", 2}, {"c", 3}, {"d", 4},
    };
    private void CollectionFixBtnCall()
    {
        foreach (int elem in intList)
        {
            Debug.Log("C# -- element: " + elem);
        }

        foreach (var pair in dic)
        {

        }
    }
}
