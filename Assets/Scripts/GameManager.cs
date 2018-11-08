using UnityEngine;

public class GameManager : SingletonUnity<GameManager>
{
    void Start()
    {
        Init();
        // XLua初始化
        XLuaManager.Instance.Init();
    }

    // 初始化
    private void Init()
    {
        UIManager.Instance.Init();          // UI管理器初始化
        DontDestroyOnLoad(gameObject);      // 永不销毁

        UIManager.Instance.Show("LoadingPanel");
    }

    public void EnterHomeScene()
    {

    }

    // 测试热修复的场景
    public void EnterHotfixTestScene()
    {
        UIManager.Instance.Show("HotfixPanel");
    }
}
