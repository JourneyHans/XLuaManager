using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptPanel : UIBase
{
    private Text titleTxt;     // 标题
    private Text contentTxt;   // 内容
    private Button confirmBtn;      // 确认按钮
    private Button cancelBtn;       // 取消按钮
    private Action confirmCallback; // 确认回调
    private Action cancelCallback;  // 取消按钮

    void Awake()
    {
        titleTxt = transform.Find<Text>("BGMask/Image/Title/Text");
        contentTxt = transform.Find<Text>("BGMask/Image/Text");
        confirmBtn = transform.Find<Button>("BGMask/Image/ButtonList/ButtonConfirm");
        confirmBtn.onClick.AddListener(ConfirmClick);
        cancelBtn = transform.Find<Button>("BGMask/Image/ButtonList/ButtonCancel");
        cancelBtn.onClick.AddListener(CancelClick);
    }

    public void SetTitleAndContent(string title, string content)
    {
        titleTxt.text = title;
        contentTxt.text = content;

        titleTxt.gameObject.SetActive(!string.IsNullOrEmpty(title));
        contentTxt.gameObject.SetActive(!string.IsNullOrEmpty(content));
    }

    /// <summary>
    /// 外部调用接口，设置Callback
    /// </summary>
    public void SetCallback(Action confirm, Action cancel)
    {
        confirmCallback = confirm;
        cancelCallback = cancel;

        confirmBtn.gameObject.SetActive(confirmCallback != null);
        cancelBtn.gameObject.SetActive(cancelCallback != null);
    }

    // 点击确认
    private void ConfirmClick()
    {
        if (confirmCallback != null)
        {
            confirmCallback();
            confirmCallback = null;
        }
        Close();
    }

    // 点击取消
    private void CancelClick()
    {
        if (cancelCallback != null)
        {
            cancelCallback();
            cancelCallback = null;
        }
        Close();
    }
}
