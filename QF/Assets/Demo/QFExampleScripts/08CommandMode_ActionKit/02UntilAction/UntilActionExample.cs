using QFramework;
using UnityEngine;
public class UntilActionExample : MonoBehaviour
{
    void Start()
    {
        var untilAction = UntilAction.Allocate(() => Input.GetMouseButtonDown(0));

        untilAction.OnEndedCallback = () =>
        {
            Debug.Log("鼠标按钮点击了"); 

        };

        this.ExecuteNode(untilAction);
        Simplify();

    }
    /// <summary>
    /// 1.其实 Until 就是用来做一些检测的工作的 Action
    /// 2.Until 可以和 SequenceNode 等节点配合使用
    /// </summary>
    void Simplify()
    {
        this.Sequence()
            .Until(() => Input.GetMouseButtonDown(0))
            .Event(() => Debug.Log("鼠标按钮点击了"))
            .Begin();
    }
}
