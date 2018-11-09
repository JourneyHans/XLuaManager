using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI基类
public class UIBase : MonoBehaviour
{
    public string UIName { set; get; }      // 对应UI名称（Prefab名称）
    public GraphicRaycaster Raycast { set; get; }
    public Canvas UICanvas { set; get; }    // 控制层级的Canvas
    public int SortOrder { set; get; }      // 层级Order
    // 关闭方法
    protected virtual void Close()
    {
        UIManager.Instance.Close(UIName);
    }
}
