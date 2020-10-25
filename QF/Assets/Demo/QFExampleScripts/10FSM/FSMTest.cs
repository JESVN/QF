using UnityEngine;
using QFramework;
/// <summary>
///玩家状态事件定义
/// </summary>
public static class PlayerStateEvent
{
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
        /// <summary>
        /// 攻击
        /// </summary>
        attack,
        /// <summary>
        /// 死亡
        /// </summary>
        die,
    }
    /// <summary>
    /// 执行PlayerState枚举对应的事件的枚举
    /// </summary>
    public enum PlayerEvent
    {
        /// <summary>
        ///待机到跑动的执行
        /// </summary>
        idleToRunEvent,
        /// <summary>
        ///待机到跳跃的执行
        /// </summary>
        idleToJumpEvent,
        /// <summary>
        ///待机到攻击的执行
        /// </summary>
        idleToAttackEvent,
        /// <summary>
        /// 跑动到跳跃的执行
        /// </summary>
        runToJumpEvent,
        /// <summary>
        ///跑动到攻击的执行
        /// </summary>
        runToAttackEvent,
        /// <summary>
        ///跑动到待机的执行
        /// </summary>
        runToIdleEvent,
        /// <summary>
        ///攻击到待机的执行
        /// </summary>
        attackToIdleEvent,
        /// <summary>
        ///待机到死亡的执行
        /// </summary>
        idleToDieEvent,
        /// <summary>
        ///跑动到死亡的执行
        /// </summary>
        runToDieEvent,
        /// <summary>
        ///跳跃到死亡的执行
        /// </summary>
        jumpToDieEvent,
        /// <summary>
        ///攻击到死亡的执行
        /// </summary>
        attackToDieEvent,
    }
}
public class FSMTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FSM<PlayerStateEvent.PlayerState, PlayerStateEvent.PlayerEvent> fsm = new FSM<PlayerStateEvent.PlayerState, PlayerStateEvent.PlayerEvent>();
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.idle,PlayerStateEvent.PlayerEvent.idleToRunEvent,PlayerStateEvent.PlayerState.run,
            data => { "待机到跑动的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.idle,PlayerStateEvent.PlayerEvent.idleToJumpEvent,PlayerStateEvent.PlayerState.jump,
            data => { "待机到跳跃的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.idle,PlayerStateEvent.PlayerEvent.idleToAttackEvent,PlayerStateEvent.PlayerState.attack,
            data => { "待机到攻击的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.run,PlayerStateEvent.PlayerEvent.runToJumpEvent,PlayerStateEvent.PlayerState.jump,
            data => { "跑动到跳跃的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.run,PlayerStateEvent.PlayerEvent.runToAttackEvent,PlayerStateEvent.PlayerState.attack,
            data => { "跑动到攻击的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.run,PlayerStateEvent.PlayerEvent.runToIdleEvent,PlayerStateEvent.PlayerState.idle,
            data => { "跑动到待机的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.attack,PlayerStateEvent.PlayerEvent.attackToIdleEvent,PlayerStateEvent.PlayerState.idle,
            data => { "攻击到待机的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.idle,PlayerStateEvent.PlayerEvent.idleToDieEvent,PlayerStateEvent.PlayerState.die,
            data => { "待机到死亡的执行".LogInfo();});
        fsm.AddTransition(PlayerStateEvent.PlayerState.run,PlayerStateEvent.PlayerEvent.runToDieEvent,PlayerStateEvent.PlayerState.die,
            data => { "跑动到死亡的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.jump,PlayerStateEvent.PlayerEvent.jumpToDieEvent,PlayerStateEvent.PlayerState.die,
            data => { "跳跃到死亡的执行".LogInfo();});
        
        fsm.AddTransition(PlayerStateEvent.PlayerState.attack,PlayerStateEvent.PlayerEvent.attackToDieEvent,PlayerStateEvent.PlayerState.die,
            data => { "攻击到死亡的执行".LogInfo();});
        fsm.Start(PlayerStateEvent.PlayerState.attack);
    }
}
