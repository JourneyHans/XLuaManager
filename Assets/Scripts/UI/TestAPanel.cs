﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAPanel : UIBase
{
    private Button BackBtn;

    void Awake()
    {
        BackBtn = transform.Find("Back").GetComponent<Button>();
        BackBtn.onClick.AddListener(Close);
    }
}
