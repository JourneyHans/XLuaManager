using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: Singleton<UIManager>
{
    private GameObject _uiCamera;               // UI相机
    private GameObject _canvasGameObject;       // UI根节点
    private GameObject _eventSystemGameObject;  // 事件接收器

    private Dictionary<string, UIBase> _uiDic = new Dictionary<string, UIBase>();   // UI字典
    private string _uiRootPath = "Assets/UI/";      // UI预制件的路径

    // UI界面的Canvas层级
    public enum SortOrderLayer
    {
        Zero = 0,
        HomePanel = 10,         // 默认以HomePanel为“基底”，每新开一个界面，默认+10
        Prompt = 100,
    }

    // 初始化
    public void Init()
    {
        _uiCamera = GameObject.Find("UICamera");
        Object.DontDestroyOnLoad(_uiCamera);
        _canvasGameObject = GameObject.Find("Canvas");
        Object.DontDestroyOnLoad(_canvasGameObject);
        _eventSystemGameObject = GameObject.Find("EventSystem");
        Object.DontDestroyOnLoad(_eventSystemGameObject);
    }

    /// <summary>
    /// 打开一个界面
    /// </summary>
    public void Show(string uiName, SortOrderLayer layer = SortOrderLayer.HomePanel)
    {
        GameObject uiGO = SimpleLoader.InstantiateGameObject(_uiRootPath + uiName + ".prefab");
        uiGO.transform.SetParent(_canvasGameObject.transform, false);
        UIBase uiBase = uiGO.GetComponent<UIBase>();
        uiBase.UIName = uiName;

        // 检测必要组件是否存在
        CheckComponent(layer, uiGO, uiBase);

        _uiDic.Add(uiName, uiGO.GetComponent<UIBase>());
//        CheckCurrentUIDIC();
    }

    /// <summary>
    /// 获取一个界面
    /// </summary>
    public T GetUI<T>(string uiName) where T : UIBase
    {
        if (!_uiDic.ContainsKey(uiName))
        {
            return null;
        }
        return _uiDic[uiName] as T;
    }

    // 检测必要组件
    private void CheckComponent(SortOrderLayer layer, GameObject uiGO, UIBase uiBase)
    {
        Canvas canvas = uiGO.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = uiGO.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;
        canvas.sortingOrder = GetMaxSortOrder(layer); // 每新加一个界面，sortingOrder+10

        uiBase.UICanvas = canvas;
        uiBase.SortOrder = canvas.sortingOrder;

        GraphicRaycaster raycast = uiGO.GetComponent<GraphicRaycaster>();
        if (raycast == null)
        {
            raycast = uiGO.AddComponent<GraphicRaycaster>();
        }

        uiBase.Raycast = raycast;
    }

    // 获取指定Layer当前最大的sortingOrder
    public int GetMaxSortOrder(SortOrderLayer layerType)
    {
        int maxOrder = (int)layerType;
        foreach (var uiPair in _uiDic)
        {
            if (maxOrder <= uiPair.Value.SortOrder)
            {
                maxOrder = uiPair.Value.SortOrder + 10;
            }
        }
        return maxOrder;
    }

    // 关闭一个界面
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
//        CheckCurrentUIDIC();
    }

    public void CheckCurrentUIDIC()
    {
        Debug.Log("=========== Check UI ===========");
        Debug.Log("\tUI count: " + _uiDic.Count);
        foreach (var uiPair in _uiDic)
        {
            Debug.Log("\tName: " + uiPair.Key);
            Debug.Log("\tSortOrder: " + uiPair.Value.SortOrder);
        }
        Debug.Log("================================");
    }
}
