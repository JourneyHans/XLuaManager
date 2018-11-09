using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAPanel : UIBase
{
    private Button BackBtn;
    private Button Btn_ShowB;

    void Awake()
    {
        BackBtn = transform.Find("Back").GetComponent<Button>();
        BackBtn.onClick.AddListener(Close);

        Btn_ShowB = transform.Find("ShowB").GetComponent<Button>();
        Btn_ShowB.onClick.AddListener(ShowBCall);
    }

    void ShowBCall()
    {

    }
}
