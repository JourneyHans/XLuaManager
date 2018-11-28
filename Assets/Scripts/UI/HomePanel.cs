using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : UIBase
{
    private Transform BtnList;
    private Button HotfixTest_Btn;
    private Button UITest_Btn;

    void Awake()
    {
        BtnList = transform.Find("ScrollView/Viewport/Content");
        HotfixTest_Btn = BtnList.Find("HotFixTest").GetComponent<Button>();
        HotfixTest_Btn.onClick.AddListener(ShowHotFixTestPanel);

        UITest_Btn = BtnList.Find("UITest").GetComponent<Button>();
        UITest_Btn.onClick.AddListener(ShowUITestPanel);
    }

    void ShowHotFixTestPanel()
    {
        UIManager.Instance.Show("HotfixPanel");
    }

    void ShowUITestPanel()
    {
        UIManager.Instance.Show("UITestPanel");
    }
}
