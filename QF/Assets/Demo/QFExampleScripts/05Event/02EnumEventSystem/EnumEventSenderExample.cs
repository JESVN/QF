using QFramework;
using UnityEngine;
public class EnumEventSenderExample : MonoBehaviour
{
    private void Update()
    {
        QEventSystem.SendEvent(TestEvent.TestOne, "TestEvent.TestOne!");
        QEventSystem.SendEvent(TestEvent.Start, "TestEvent.Start!");
        QEventSystem.SendEvent(TestEvent.End, "TestEvent.End!");
        QEventSystem.SendEvent(TestEventB.TestB, "TestEventB.TestB!");
        QEventSystem.SendEvent(TestEventB.Start, "TestEventB.Start!");
        QEventSystem.SendEvent(TestEventB.End, "TestEventB.End!");
    }
}
