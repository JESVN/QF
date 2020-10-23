using QFramework;
using UnityEngine;
public class QFSingleTest : MonoBehaviour,ISingleton
{
    public static QFSingleTest Instance
    {
        get { return MonoSingletonProperty<QFSingleTest>.Instance;}
    }

    public void OnSingletonInit()
    {
        Debug.Log($"QF单例初始化完成");
    }
}
