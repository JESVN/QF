using QFramework;
using UnityEngine;

#region 事件定义
public enum TestEvent
{
    Start,
    TestOne,
    End,
}
public enum TestEventB
{
    Start = TestEvent.End, // 为了保证每个消息 Id 唯一，需要头尾相接
    TestB,
    End,
}
#endregion 事件定义
/// <summary>
/// 基于枚举的事件系统—EnumEventSystem
/// </summary>
public class EnumEventSystemExample : MonoBehaviour
{
    void Start()
    {
        QEventSystem.RegisterEvent(TestEvent.Start, OnEvnt2);
        QEventSystem.RegisterEvent(TestEvent.TestOne, OnEvent);
        QEventSystem.RegisterEvent(TestEvent.End, OnEvnt3);
        QEventSystem.RegisterEvent(TestEventB.Start, OnEvnt4);
        QEventSystem.RegisterEvent(TestEventB.TestB, OnEvnt5);
        QEventSystem.RegisterEvent(TestEventB.End, OnEvnt6);
    }

    void OnEvent(int key, params object[] obj)
    {
        switch (key)
        {
            case (int) TestEvent.TestOne:
                obj[0].LogInfo();
                break;
        }
    }
    void OnEvnt2(int key, params object[] obj)
    {
        Debug.Log($"{key},{obj[0]}");
    }
    void OnEvnt3(int key, params object[] obj)
    {
        Debug.Log($"{key},{obj[0]}");
    }
    void OnEvnt4(int key, params object[] obj)
    {
        Debug.Log($"{key},{obj[0]}");
    }
    void OnEvnt5(int key, params object[] obj)
    {
        Debug.Log($"{key},{obj[0]}");
    }
    void OnEvnt6(int key, params object[] obj)
    {
        Debug.Log($"{key},{obj[0]}");
    }
    private void OnDestroy()
    {
        QEventSystem.UnRegisterEvent(TestEvent.Start, OnEvent);
        QEventSystem.UnRegisterEvent(TestEvent.TestOne, OnEvent);
        QEventSystem.UnRegisterEvent(TestEvent.End, OnEvent);
        QEventSystem.UnRegisterEvent(TestEventB.Start, OnEvent);
        QEventSystem.UnRegisterEvent(TestEventB.TestB, OnEvent);
        QEventSystem.UnRegisterEvent(TestEventB.End, OnEvent);
        Debug.Log($"注销基于枚举的事件");
    }
}
