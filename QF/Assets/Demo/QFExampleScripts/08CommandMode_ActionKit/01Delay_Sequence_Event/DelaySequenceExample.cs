using System;
using QFramework;
using UnityEngine;
public class DelaySequenceExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FinalSimplify();
    }
    /// <summary>
    /// 最终简化版本
    /// </summary>
    void FinalSimplify()
    {
        this.Sequence()
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Delay(1.0f).Event(() => Debug.Log("延时 1 秒" + DateTime.Now))
            .Begin();
    }
}
