using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : UIBase
{
    private Transform BtnList;
    private Button LoadModelBtn;
    void Awake()
    {
        BtnList = transform.Find("ScrollView/Viewport/Content");

        transform.Find<Button>("Back").onClick.AddListener(Close);

        LoadModelBtn = BtnList.Find<Button>("LoadModel");
        LoadModelBtn.onClick.AddListener(LoadModelBtnCall);
    }

    private void LoadModelBtnCall()
    {
        Debug.Log("Click LoadModelBtn");
    }
}
