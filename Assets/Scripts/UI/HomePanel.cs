using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : UIBase
{
    private Transform BtnList;
    private Button HotfixTest_Btn;
    private Button UITest_Btn;
    private Button CharacterTest_Btn;
    private Button AssetbundleTest_Btn;

    void Awake()
    {
        BtnList = transform.Find("ScrollView/Viewport/Content");

        HotfixTest_Btn = BtnList.Find<Button>("HotFixTest");
        HotfixTest_Btn.onClick.AddListener(ShowHotFixTestPanel);

        UITest_Btn = BtnList.Find<Button>("UITest");
        UITest_Btn.onClick.AddListener(ShowUITestPanel);

        CharacterTest_Btn = BtnList.Find<Button>("CharacterTest");
        CharacterTest_Btn.onClick.AddListener(ShowCharacterPanel);
    }

    void ShowHotFixTestPanel()
    {
        UIManager.Instance.Show<HotfixPanel>();
    }

    void ShowUITestPanel()
    {
        UIManager.Instance.Show<UITestPanel>();
    }

    void ShowCharacterPanel()
    {
        UIManager.Instance.Show<CharacterPanel>();
    }
}
