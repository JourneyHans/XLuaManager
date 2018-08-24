using UnityEngine;

public class PathManager
{
    /// <summary>
    /// 获取Resource路径
    /// </summary>
    public static string GetResourcePath()
    {
        return Application.dataPath + "/Resources/";
    }

    /// <summary>
    /// 获取可读可写路径
    /// </summary>
    public static string GetPersistentIOPath()
    {
#if UNITY_ANDROID
        return Application.persistentDataPath + "/";
#elif UNITY_IOS
        return Application.persistentDataPath + "/";
#endif
        return "";
    }

    /// <summary>
    /// 获取可读可写路径，通过WWW的方式
    /// </summary>
    public static string GetPersistentWWWPath()
    {
#if UNITY_ANDROID
        return "file:///" + GetPersistentIOPath();
#elif UNITY_IOS
        return "file://" + GetPersistentIOPath();
#endif
        return "";
    }

    /// <summary>
    /// 获取streamingassets路径，通过WWW的方式
    /// </summary>
    public static string GetLocalStreamingWWWPath()
    {
#if UNITY_EDITOR && !UNITY_EDITOR_OSX
        return "file:///" + Application.streamingAssetsPath + "/";
#elif UNITY_IOS
            return "file://" + Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID
            return Application.streamingAssetsPath + "/";
#endif
        return "";
    }
}
