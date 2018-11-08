using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLoader
{
    public static T Load<T>(string path) where T : Object
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
        // TODO：正式需要读取Assetbundle或Resources
#endif
    }

    /// <summary>
    /// 加载预制并实例化
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="parent">父节点</param>
    /// <param name="resetTransform">重制Transform</param>
    public static GameObject InstantiateGameObject(string path, Transform parent = null, bool resetTransform = true)
    {
        GameObject prefab = Load<GameObject>(path);
        if (prefab)
        {
            GameObject gameObject = GameObject.Instantiate(prefab);
            if (parent != null)
            {
                gameObject.transform.SetParent(parent);
            }
            if (resetTransform)
            {
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localEulerAngles = Vector3.zero;
                gameObject.transform.localScale = Vector3.one;
            }
            return gameObject;
        }
        return null;
    }
}
