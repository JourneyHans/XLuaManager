using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <param name="callback">完成回调</param>
    public void LoadScene(string sceneName, Callback finishCallback)
    {
        GameManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, finishCallback));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, Callback finishCallback)
    {
        var asyn = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyn.allowSceneActivation = false;
        while (!asyn.isDone)
        {
            if (asyn.progress >= 0.9f)
            {
                asyn.allowSceneActivation = true;
            }
            yield return null;
        }

        if (finishCallback != null)
        {
            finishCallback();
        }
    }
}
