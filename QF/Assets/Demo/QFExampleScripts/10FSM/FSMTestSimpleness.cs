using UnityEngine;
using QFramework;
/// <summary>
/// 状态枚举
/// </summary>
public enum PlayerState
{
    /// <summary>
    /// 待机
    /// </summary>
    idle,
    /// <summary>
    /// 跑
    /// </summary>
    run,
    /// <summary>
    /// 跳
    /// </summary>
    jump,
}
/// <summary>
/// 执行PlayerState枚举对应的事件的枚举
/// </summary>
public enum PlayerEvent
{
    /// <summary>
    /// 跑转待机事件
    /// </summary>
    runToIdleEvent,
    /// <summary>
    /// 跳转待机事件
    /// </summary>
    jumpToIdleEvent,
    /// <summary>
    /// 跑事件
    /// </summary>
    toRunEvent,
    /// <summary>
    /// 跳事件
    /// </summary>
    toJumpEvent,
}
public class FSMTestSimpleness : MonoBehaviour
{
    private FSM<PlayerState, PlayerEvent> fsm;
    // Start is called before the first frame update
    void Start()
    {
       fsm=new FSM<PlayerState, PlayerEvent>((x,y) =>
        {
            //Debug.Log($"{x+","+y}");    
        });
        fsm.AddTransition(PlayerState.idle,PlayerEvent.toRunEvent,PlayerState.run,
            delegate(object[] objects)
            {
                Debug.Log($"{objects[0]}");
            });
        fsm.AddTransition(PlayerState.idle,PlayerEvent.toJumpEvent,PlayerState.jump,
            delegate(object[] objects)
            {
                Debug.Log($"{objects[0]}");
            });
        fsm.AddTransition(PlayerState.run,PlayerEvent.runToIdleEvent,PlayerState.idle,
            delegate(object[] objects)
            {
                Debug.Log($"{objects[0]}");
            });
        fsm.AddTransition(PlayerState.jump,PlayerEvent.jumpToIdleEvent,PlayerState.idle,
            delegate(object[] objects)
            {
                Debug.Log($"{objects[0]}");
            });
        fsm.Start(PlayerState.idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fsm.HandleEvent(PlayerEvent.toJumpEvent,"跳");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fsm.HandleEvent(PlayerEvent.jumpToIdleEvent,"待机");
        }
        if (Input.GetKey(KeyCode.D))
        {
            fsm.HandleEvent(PlayerEvent.toRunEvent,"跑");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            fsm.HandleEvent(PlayerEvent.runToIdleEvent,"待机");
        }
    }
}
