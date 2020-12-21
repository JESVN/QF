using QFramework;
using UnityEngine;
public class GameControls : MonoSingleton<GameControls>
{
    public void Creat()
    {
        Debug.Log($"创建方块");
        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    public override void OnSingletonInit()
    {
        Debug.Log($"初始化成功");
    }
}
