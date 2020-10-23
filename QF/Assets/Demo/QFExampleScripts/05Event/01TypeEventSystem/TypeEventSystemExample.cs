using UnityEngine;
using QFramework;
#region 事件定义
public class GameStartEvent
{
}
public class GameOverEvent
{
    // 可以携带参数
    public int Score;
}
public interface ISkillEvent
{
    void ReleaseSkills();
}
// 支持继承
public class PlayerSkillAEvent : ISkillEvent
{
    private string skillName;

    public PlayerSkillAEvent(string skillName)
    {
        this.skillName = skillName;
    }
    public void ReleaseSkills()
    {
        Debug.Log($"技能释放:{skillName}A");
    }
}
/// <summary>
/// 基于类型的事件系统—TypeEventSystem
/// </summary>
public class PlayerSkillBEvent : ISkillEvent
{
    private string skillName;
    public PlayerSkillBEvent(string skillName)
    {
        this.skillName = skillName;
    }
    public void ReleaseSkills()
    {
        Debug.Log($"技能释放:{skillName}B");
    }
}
#endregion
public class TypeEventSystemExample : MonoBehaviour
{
    private void Start()
    {
        TypeEventSystem.Register<GameStartEvent>(OnGameStartEvent);
        TypeEventSystem.Register<GameOverEvent>(OnGameOverEvent);
        TypeEventSystem.Register<ISkillEvent>(OnSkillEvent);
        TypeEventSystem.Send<GameStartEvent>();
        // 要把事件发送给父类
        TypeEventSystem.Send<ISkillEvent>(new PlayerSkillAEvent("天马流星拳"));
        TypeEventSystem.Send<ISkillEvent>(new PlayerSkillBEvent("地狱风火轮"));
        TypeEventSystem.Send(new GameOverEvent()
        {
            Score = 100
        });
    }

    void OnGameStartEvent(GameStartEvent gameStartEvent)
    {
        Debug.Log("游戏开始了");
    }

    void OnGameOverEvent(GameOverEvent gameOverEvent)
    {
        Debug.LogFormat("游戏结束，分数:{0}", gameOverEvent.Score);
    }

    void OnSkillEvent(ISkillEvent skillEvent)
    {
        skillEvent.ReleaseSkills();
    }

    private void OnDestroy()
    {
        TypeEventSystem.UnRegister<GameStartEvent>(OnGameStartEvent);
        TypeEventSystem.UnRegister<GameOverEvent>(OnGameOverEvent);
        TypeEventSystem.UnRegister<ISkillEvent>(OnSkillEvent);
        Debug.Log($"注销基于类型的事件");
    }
}
