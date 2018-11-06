using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadManager : Singleton<DownloadManager>
{
    /// <summary>
    /// 外部调用的下载方法
    /// </summary>
    /// <param name="url">文件路径</param>
    /// <param name="finishCallback">完成后回调</param>
    public void Download(string url, Action<WWW> finishCallback)
    {
        GameManager.Instance.StartCoroutine(DownloadCoroutine(url, finishCallback));
    }

    /// 实际调用的下载方法，Coroutine方式
    private IEnumerator DownloadCoroutine(string url, Action<WWW> finishCallback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error) && www.isDone)
        {
            if (finishCallback != null)
            {
                finishCallback(www);
            }
        }
        else
        {
            Debug.LogError("Download failed: " + www.error);
            finishCallback(null);
        }

        www.Dispose();
    }
}
