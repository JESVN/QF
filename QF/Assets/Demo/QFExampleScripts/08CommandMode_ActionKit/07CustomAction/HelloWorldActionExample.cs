using UnityEngine;
using QFramework;
/// <summary>
/// 自定义Action
/// </summary>
public class SayHelloWorld : Action
{
    protected override void OnBegin()
    {
        Debug.Log("自定义Hello World !");

        // 结束此 Action
        Finish();
    }
}
public class HelloWorldActionExample : MonoBehaviour
{
    void Start()
    {
        this.ExecuteNode( new SayHelloWorld());
        this.Sequence()
            .Delay(3f)
            .Append(new SayHelloWorld())
            .Begin();
    }
}
