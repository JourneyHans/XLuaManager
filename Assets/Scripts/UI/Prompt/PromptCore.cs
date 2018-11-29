using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptCore : Singleton<PromptCore>
{
    public void Show(string title, string content, Action confirmCall = null, Action cancelCall = null)
    {
        PromptPanel panel = UIManager.Instance.Show<PromptPanel>(UIManager.OpenType.Add, UIManager.SortOrderLayer.Prompt);
        panel.SetTitleAndContent(title, content);
        panel.SetCallback(confirmCall, cancelCall);
    }

    public void Close()
    {
        UIManager.Instance.Close<PromptPanel>();
    }
}
