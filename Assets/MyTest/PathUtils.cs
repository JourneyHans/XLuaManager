using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathUtils
{
    public static readonly string STREAMING_ASSET_PATH =
#if UNITY_EDITOR
        Application.dataPath + "/StreamingAssets"; //PC平台
#elif UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets";   // 安卓平台
#elif UNITY_IOS
        Application.dataPath + "/Raw"; // IOS平台
#endif
}
