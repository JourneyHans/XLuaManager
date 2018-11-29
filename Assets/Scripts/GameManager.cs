using UnityEngine;

public class GameManager : SingletonUnity<GameManager>
{
    [Tooltip("XLua启动开关")]
    public bool EnableXLua;

    [Tooltip("本地模式开关，一般开发情况下为true，正式上线为false")]
    public bool XLuaLocalMode;

    [Tooltip("XLua调试模式开关\n*打正式包的时候关闭，打完正式包后开启，并紧接着打一个测试包\n*当热修复bug的时候，在lua端调用这个变量，现在测试包中跑过没问题后\n*再去掉这个变量判断让正式包也生效")]
    public bool XLuaTestPack;

    void Start()
    {
        Init();

        if (EnableXLua)
        {
            // XLua初始化
            XLuaManager.Instance.Init();
        }
        else
        {
            Messenger<float>.Invoke("UpdateResource", 1f);
        }
    }

    // 初始化
    private void Init()
    {
        UIManager.Instance.Init();          // UI管理器初始化
        DontDestroyOnLoad(gameObject);      // 永不销毁

        UIManager.Instance.Show<LoadingPanel>(UIManager.OpenType.Add, UIManager.SortOrderLayer.Zero);
    }

    public void EnterHomeScene()
    {

    }

    public void EnterUITestScene()
    {
        UIManager.Instance.Show<HomePanel>();
    }

    // 测试热修复的场景
    public void EnterHotfixTestScene()
    {
        UIManager.Instance.Show<HomePanel>();
    }
}
