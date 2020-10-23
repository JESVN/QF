using QFramework;
using UnityEngine;
using QFramework.Example;
public static class UIEventID
{
    public enum MenuPanel
    {
        //ui事件ID为3000～5999，见QMsgSpan
        Start =QMgrID.UI,
        ChangeMainColor,
        ChangeGameingColor,
        End,
    }

    public enum SomeEvent
    {
        Start = MenuPanel.End,
        SomeOperation,
        End
    }
}
/// <summary>
/// UI的消息事件机制底层基于EnumEventSystem
/// </summary>
public class UIKitEventSystemExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResMgr.Init();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,150,50),"打开GameEvent" ))
        {
            UIKit.OpenPanel<GameEvent>();
            UIKit.OpenPanel<ClickPanel>();
        }
    }
}
