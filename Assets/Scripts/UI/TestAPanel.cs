using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAPanel : UIBase
{
    private Button BackBtn;

    void Awake()
    {
        BackBtn = transform.Find<Button>("Back");
        BackBtn.onClick.AddListener(Close);
    }
}
