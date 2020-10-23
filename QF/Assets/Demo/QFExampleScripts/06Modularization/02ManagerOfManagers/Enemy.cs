using QFramework;
using UnityEngine;
public class Enemy : QMonoBehaviour
{
    public override IManager Manager
    {
        get { return EnemyManager.Instance; }
    }
    public void PlaySkill(string skillName)
    {
        Debug.Log(this.name + ":" + skillName);
    }
}
