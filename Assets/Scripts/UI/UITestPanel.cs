using System;
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

    private int inputKcount = 0;
    private string[] dotList = { "", ".", "..", "..." };
    private string[] contentList = { "", "x2", "x3", "x4" };

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
        UIManager.Instance.Show<TestAPanel>();
    }

    void ShowByReplaceCallback()
    {
        UIManager.Instance.Show<TestAPanel>(UIManager.OpenType.Replace);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            int index = inputKcount % 4;
            inputKcount++;
            PromptCore.Instance.Show("标题" + dotList[index], "内容" + contentList[index], PromptConfirmCall, PromptCancelCall);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            inputKcount = 0;
            PromptCore.Instance.Close();
        }
    }

    void PromptConfirmCall()
    {
        Debug.Log("点击了确认");
    }

    void PromptCancelCall()
    {
        Debug.Log("点击了取消");
    }
}
