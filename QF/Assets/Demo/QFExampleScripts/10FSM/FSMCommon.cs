using UnityEngine;
using QFramework;
public class FSMCommon : MonoBehaviour
{
    /// <summary>
    /// 待机状态
    /// </summary>
    class IdleState: ActionKitFSMState<FSMCommon>
    {
        public IdleState(FSMCommon target): base(target)
        {
            
        }
        protected override void OnEnter()
        {
            Debug.Log($"进入待机状态");
        }
        protected override void OnExit()
        {
            Debug.Log($"退出待机状态");
        }
        protected override void OnUpdate()
        {
            Debug.Log($"更新待机状态");
        }
    }
    /// <summary>
    /// 跑状态
    /// </summary>
    class RunState: ActionKitFSMState<FSMCommon>
    {
        private FSMCommon _fsmCommon;
        public RunState(FSMCommon target):base(target)
        {
            _fsmCommon = target;
        }
        protected override void OnEnter()
        {
            Debug.Log($"进入跑状态");
        }
        protected override void OnExit()
        {
            Debug.Log($"退出跑状态");
        }
        protected override void OnUpdate()
        {
            _fsmCommon.player.Run(_fsmCommon.speed,_fsmCommon.orientation);
            Debug.Log($"更新跑状态");
        }
    }
    /// <summary>
    /// 跳状态
    /// </summary>
    class JumpState: ActionKitFSMState<FSMCommon>
    {
        private FSMCommon _fsmCommon;
        public JumpState(FSMCommon target):base(target)
        {
            _fsmCommon = target;
        }
        protected override void OnEnter()
        {
            _fsmCommon.player.Jump(_fsmCommon.size);
            Debug.Log($"进入跳状态");
        }
        protected override void OnExit()
        {
            Debug.Log($"退出跳状态");
        }
        protected override void OnUpdate()
        {
            Debug.Log($"更新跳状态");
        }
    }
    /// <summary>
    /// 转换条件按键ADWS->idle-run
    /// </summary>
    class KeyADWS: ActionKitFSMTransition<IdleState,RunState>
    {
        
    }
    /// <summary>
    /// 转换条件按键抬起ADWS->run-idle
    /// </summary>
    class KeyADWSUp: ActionKitFSMTransition<RunState,IdleState>
    {
        
    }
    /// <summary>
    /// 转换条件按键空格->idle-jump
    /// </summary>
    class KeySpaceDown: ActionKitFSMTransition<IdleState,JumpState>
    {
        
    }
    /// <summary>
    /// 转换条件按键空格抬起->jump-idle
    /// </summary>
    class KeySpaceUp: ActionKitFSMTransition<JumpState,IdleState>
    {
        
    }
    /// <summary>
    /// 转换条件跑状态到跳状态
    /// </summary>
    class KeyADWSpaceDown: ActionKitFSMTransition<RunState,JumpState>
    {
        
    }
    /// <summary>
    /// 转换条件跳状态到跑状态
    /// </summary>
    class KeySpaceADWS: ActionKitFSMTransition<JumpState,RunState>
    {
        
    }
    /// <summary>
    /// 跳跃大小
    /// </summary>
    private float size;
    /// <summary>
    /// 移动速度大小
    /// </summary>
    private float speed;
    /// <summary>
    /// 方向
    /// </summary>
    private Vector3 orientation;
    /// <summary>
    /// 玩家控制
    /// </summary>
    [SerializeField] private PlayerControls player;
    /// <summary>
    /// 状态机
    /// </summary>
    ///
    ///
    ///
    /// 
    private ActionKitFSM fsm=new ActionKitFSM();
    // Start is called before the first frame update
    void Start()
    {
        //实例化状态
        IdleState idleState=new IdleState(this);
        RunState runState=new RunState(this);
        JumpState jumpState=new JumpState(this);
        //添加状态
        fsm.AddState(idleState);
        fsm.AddState(runState);
        fsm.AddState(jumpState);
        //添加转换条件
        fsm.AddTransition(new KeyADWS());
        fsm.AddTransition(new KeyADWSUp());
        fsm.AddTransition(new KeySpaceDown());
        fsm.AddTransition(new KeySpaceUp());
        fsm.AddTransition(new KeyADWSpaceDown());
        fsm.AddTransition(new KeySpaceADWS());
        //启动默认状态
        fsm.StartState<IdleState>();
        size = 500f;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        orientation = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            orientation += Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            orientation += Vector3.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            orientation += Vector3.back;
        }
        if (Input.GetKey(KeyCode.S))
        {
            orientation += Vector3.forward;
        }       
        if(orientation!=Vector3.zero)
        {
            if (fsm.CurrentState is IdleState)
            {
                fsm.HandleEvent<KeyADWS>();
            }
            else if (fsm.CurrentState is JumpState)
            {
                fsm.HandleEvent<KeySpaceADWS>();
            }
            orientation=orientation.normalized;
            fsm.Update();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fsm.CurrentState is IdleState)
            {
                fsm.HandleEvent<KeySpaceDown>();
            }
            else if (fsm.CurrentState is RunState)
            {
                fsm.HandleEvent<KeyADWSpaceDown>();
            }
        }
        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.S))
        {
            fsm.HandleEvent<KeyADWSUp>();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fsm.HandleEvent<KeySpaceUp>();
        }
    }
}
