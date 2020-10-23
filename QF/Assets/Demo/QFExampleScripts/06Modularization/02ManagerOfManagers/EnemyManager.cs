using System.Collections.Generic;
using QFramework;
using UnityEngine;
[MonoSingletonPath("[Game]/EnemyManager")]
public class EnemyManager : QMgrBehaviour,ISingleton
{
    public override int ManagerId
    {
        get { return QMgrID.Enemy; }
    }
    public void OnSingletonInit()
    {
        Debug.Log($"单例初始化完成[{name}](只执行一次)");
    }
    public static EnemyManager Instance
    {
        get { return MonoSingletonProperty<EnemyManager>.Instance;}
    }

    protected override void ProcessMsg(int eventId, QMsg msg)
    {
        if (eventId == (int) EnemyEvent.SkillEvent.Play)
        {
            var enemySkillPlay = msg as EnemySkillPlay;
            if (mEnemies.ContainsKey(enemySkillPlay.EnemyId))
            {
                var enemy = mEnemies[enemySkillPlay.EnemyId];
                enemy.PlaySkill(enemySkillPlay.SkillName);
            }
            else
            {
                Debug.Log(enemySkillPlay.EnemyId + ":" + enemySkillPlay.SkillName);
            }
        }
    }
    private Dictionary<string,Enemy> mEnemies = new Dictionary<string, Enemy>();

    private void Awake()
    {
        CreateEnemy("123");
        CreateEnemy("456");
        CreateEnemy("789");
    }

    void CreateEnemy(string enemyId)
    {
        var enemyObj = new GameObject("Enemy" + enemyId);
        var enemyScript = enemyObj.AddComponent<Enemy>();

        mEnemies.Add(enemyId, enemyScript);
    }
}   
