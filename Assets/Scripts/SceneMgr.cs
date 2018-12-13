using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    private Callback _finishCallback;
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <param name="callback">完成回调</param>
    public void LoadScene(string sceneName, Callback callback = null)
    {
        _finishCallback = callback;
        GameManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
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

        LoadFinish();
    }

    private void LoadFinish()
    {
        if (_finishCallback != null)
        {
            _finishCallback();
            _finishCallback = null;
        }

#if UNITY_EDITOR
        ResetAllMaterials();
#endif
    }

    private void ResetAllMaterials()
    {
        Renderer[] renders = Object.FindObjectsOfType<Renderer>();

        foreach (Renderer r in renders)
        {
            Material[] materials = r.sharedMaterials;
            string[] shaders = new string[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == null)
                    continue;

                shaders[i] = materials[i].shader.name;
                materials[i].shader = Shader.Find(shaders[i]);
            }
        }

        //reset skybox
        Material mat = RenderSettings.skybox;
        if (mat != null)
            mat.shader = Shader.Find(mat.shader.name);
    }
}
