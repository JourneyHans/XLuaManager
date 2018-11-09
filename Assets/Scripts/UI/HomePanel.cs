using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : UIBase
{
    private Button Btn_ShowA;

    void Awake()
    {
        Btn_ShowA = transform.Find("ShowA").GetComponent<Button>();
        Btn_ShowA.onClick.AddListener(ShowACall);
    }

    void ShowACall()
    {
        UIManager.Instance.Show("TestAPanel");
    }
}
