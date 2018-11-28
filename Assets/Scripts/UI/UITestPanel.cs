using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestPanel : UIBase
{
    private Button BackBtn;

    private Transform BtnList;
    private Button ShowByAddBtn;
    private Button ShowByReplaceBtn;

    void Awake()
    {
        // 返回上一个界面
        BackBtn = transform.Find("Back").GetComponent<Button>();
        BackBtn.onClick.AddListener(Close);

        BtnList = transform.Find("ScrollView/Viewport/Content");
        ShowByAddBtn = BtnList.Find("ShowByAdd").GetComponent<Button>();
        ShowByAddBtn.onClick.AddListener(ShowByAddCallback);

        ShowByReplaceBtn = BtnList.Find("ShowByReplace").GetComponent<Button>();
        ShowByReplaceBtn.onClick.AddListener(ShowByReplaceCallback);
    }

    void ShowByAddCallback()
    {
        UIManager.Instance.Show("TestAPanel");
    }

    void ShowByReplaceCallback()
    {
        UIManager.Instance.Show("TestAPanel", UIManager.OpenType.Replace);
    }
}
