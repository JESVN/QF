using QFramework;
using UnityEngine;
/// <summary>
/// 循环执行任务(可指定循环次数)
/// </summary>
public class RepeatNodeExample : MonoBehaviour
{
    void Start()
    {
        var delay = DelayAction.Allocate(1.0f, () => Debug.Log("延时 1 秒"));
        //循环10次任务
        var repeatNode = new RepeatNode(delay,10);
        
        var delay2 = DelayAction.Allocate(2.0f, () => Debug.Log("延时 2 秒"));
        //循环5次任务
        var repeatNode2 = new RepeatNode(delay2,5);

        this.ExecuteNode(repeatNode);
        this.ExecuteNode(repeatNode2);

        Simplify();
    }
    /// <summary>
    /// 简化版
    /// </summary>
    void Simplify()
    {
        //不填次数就是无限循环
        this.Repeat()
            .Delay(1f)
            .Event(() => Debug.Log("延时 1 秒执行"))
            .Begin();
    }
}
