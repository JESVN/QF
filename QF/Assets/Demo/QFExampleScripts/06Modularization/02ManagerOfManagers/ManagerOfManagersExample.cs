using QFramework;
using UnityEngine;
public class ManagerOfManagersExample : QMonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,180,50),"ManagerOfManagers案例1" ))
        {
            SendMsg(new EnemySkillPlay()
            {
                SkillName = "AOE",
                EnemyId = "12345"
            });
        }
        if (GUI.Button(new Rect(0,50,180,50),"ManagerOfManagers案例2" ))
        {
            // 可以直接用 Manager 去发送消息
            SendMsg(new EnemySkillPlay()
            {
                SkillName = "AOE",
                EnemyId = "123"
            });
        }
    }
    public override IManager Manager
    {
        get { return EnemyManager.Instance; }
    }
}
