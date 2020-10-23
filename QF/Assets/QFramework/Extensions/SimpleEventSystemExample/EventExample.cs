using QFramework;
using UnityEngine;
using UniRx;

namespace QF.PackageKit.Example
{
    public class A
    {
    }

    public class B
    {
    }
    /// <summary>
    ///基于UniRx的事件机制—SimpleEventSystem
    ///为什么SimpleEventSystem 没有内置到 QFramework 中？
    ///1.因为UniRx 比较占用包体体积，大概 1 ~ 5m 左右，对于一些比较看重包体大小的项目来说（比如棋牌），UniRx 不是必须用的。
    ///2.为解决这个问题，QFramework 把所有第三方依赖的部分都放到 Extensions 模块了，而 SimpleEventSystem 也独立作为一个模块维护了
    ///3.大家需要的时候自行下载即可
    /// </summary>
    public class EventExample : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            SimpleEventSystem.GetEvent<A>()
                .Subscribe(a => Log.I("a message"));
            
            SimpleEventSystem.GetEvent<B>()
                .Select(b=>"b message") // 支持 UniRx 的 LINQ 操作符
                .Subscribe(bMsg => Log.I(bMsg));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                SimpleEventSystem.Publish(new A());
                
            }

            if (Input.GetMouseButtonUp(1))
            {
                SimpleEventSystem.Publish(new B());
            }
        }
    }
}