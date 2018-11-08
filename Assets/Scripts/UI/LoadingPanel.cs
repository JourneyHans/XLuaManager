using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : UIBase
{
    private Image _progress;

    protected override void Awake()
    {
        base.Awake();
        _progress = transform.Find("Progress/Value").GetComponent<Image>();
        _progress.fillAmount = 0f;

        Messenger<float>.AddListener("UpdateResource", SetProgressValue);
    }

    private void SetProgressValue(float value)
    {
        _progress.fillAmount = value;

        if (Math.Abs(value - 1f) <= 0)
        {
            // 完成，转到主场景
            SceneMgr.Instance.LoadScene("HotfixTestScene", delegate
            {
                GameManager.Instance.EnterHotfixTestScene();
                Close();
            });
        }
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener("UpdateResource", SetProgressValue);
    }
}
