using UnityEngine;
using QFramework;
/// <summary>
/// 多个任务同时执行
/// </summary>
public class SpawnNodeExample : MonoBehaviour
{
    void Start()
    {
        var spawnNode = new SpawnNode();

        spawnNode.Add(DelayAction.Allocate(1.0f, () => Debug.Log(Time.time)));
        spawnNode.Add(DelayAction.Allocate(1.0f, () => Debug.Log(Time.time)));
        spawnNode.Add(DelayAction.Allocate(1.0f, () => Debug.Log(Time.time)));
        spawnNode.Add(DelayAction.Allocate(1.0f, () => Debug.Log(Time.time)));
        spawnNode.Add(DelayAction.Allocate(1.0f, () => Debug.Log(Time.time)));

        this.ExecuteNode(spawnNode);
    }
}
