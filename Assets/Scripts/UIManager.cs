using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{
    private GameObject _canvasGameObject;       // UI根节点
    private GameObject _eventSystemGameObject;  // 事件接收器

    private Dictionary<string, UIBase> _uiDic = new Dictionary<string, UIBase>();   // UI字典
    private string _uiRootPath = "Assets/UI/";      // UI预制件的路径

    public void Init()
    {
        _canvasGameObject = GameObject.Find("Canvas");
        Object.DontDestroyOnLoad(_canvasGameObject);
        _eventSystemGameObject = GameObject.Find("EventSystem");
        Object.DontDestroyOnLoad(_eventSystemGameObject);
    }

    public void Show(string uiName)
    {
        GameObject uiGO = SimpleLoader.InstantiateGameObject(_uiRootPath + uiName + ".prefab");
        uiGO.transform.SetParent(_canvasGameObject.transform, false);
        UIBase uiBase = uiGO.GetComponent<UIBase>();
        uiBase.UIName = uiName;
        _uiDic.Add(uiName, uiGO.GetComponent<UIBase>());
    }

    public void Close(string uiName)
    {
        if (!_uiDic.ContainsKey(uiName))
        {
            Debug.LogError("Can't find ui, name: " + uiName);
            Debug.Log("Current UI List: ");
            foreach (var uiPair in _uiDic)
            {
                UIBase uiBase = uiPair.Value;
                Debug.Log("ui name: " + uiBase.UIName);
            }
            return;
        }
        GameObject uiGO = _uiDic[uiName].gameObject;
        Object.Destroy(uiGO);
        _uiDic.Remove(uiName);
    }
}
