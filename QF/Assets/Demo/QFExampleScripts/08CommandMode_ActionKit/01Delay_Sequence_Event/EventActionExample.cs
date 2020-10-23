using QFramework;
using UnityEngine;
public class EventActionExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Chicken();
        Simplify();
        
    }
    /// <summary>
    /// 单独使用，看起来有点鸡肋
    /// </summary>
    void Chicken()
    {
        var eventAction = EventAction.Allocate(() => Debug.Log("执行 EventAction"));
        this.ExecuteNode(eventAction);
    }
    /// <summary>
    /// 当它与一些节点使用的时候会起非常大的作用。比如序列节点、并行节点、重复节点等
    /// 如下：
    /// </summary>
    void Simplify()
    {
        this.Sequence()
            .Delay(1.0f)
            .Event(() => Debug.Log("延时了 1 秒"))
            .Begin();
    }
}
