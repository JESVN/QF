using System.Collections;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
/// <summary>
/// 加载场景ab包不支持模拟模式，请打ab包并且取消模拟模式测试
/// </summary>
public class ResKitSceneLoad : MonoBehaviour
{
    public static ResKitSceneLoad Instance;
    private ResLoader resLoader = ResLoader.Allocate();
    private int referenceCount;
    private int _maxCount=30;
    /// <summary>
    /// 设置最大内存场景
    /// </summary>
    public int maxCount {
        get
        {
            return _maxCount;
        }
        set
        {
            if (value <= 0)
                _maxCount = 30;
            else
                _maxCount = value;
        }
    }
    static ResKitSceneLoad()
    {
        GameObject go=new GameObject("[ResKitSceneLoad]");
        Instance = go.AddComponent<ResKitSceneLoad>();
        DontDestroyOnLoad(go);
    }
    /// <summary>
    /// 同步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoad(string sceneName)
    {
        ResetApply();
        resLoader.LoadSync(sceneName);
        SceneManager.LoadScene(sceneName);
        referenceCount++;
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadAsyn(string sceneName,GameObject go=null)
    {
        if (go != null)
            go.SetActive(true);
        ResetApply();
        resLoader.Add2Load(sceneName);
        resLoader.LoadAsync(() =>
        {
            StartCoroutine(IELoadSceneAsyn(sceneName,go));
        });
    }
    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator IELoadSceneAsyn(string sceneName,GameObject go=null)
    {
        AsyncOperation asyncOperation=SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress<0.9f)
        {
            yield return null;
        }
        if (go != null)
            go.SetActive(false);
        asyncOperation.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        referenceCount++;
    }
    /// <summary>
    /// 判断重新申请
    /// </summary>
    private void ResetApply()
    {
        if (referenceCount > _maxCount)
        {
            Debug.Log($"释放Scene资源");
            resLoader.Recycle2Cache();
            resLoader.ReleaseAllRes();
            resLoader = null;
            resLoader=ResLoader.Allocate();
            referenceCount = 0;
        }
    }
}
