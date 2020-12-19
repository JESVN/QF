using UnityEngine;
public class Tools
{
    /// <summary>
    /// 获取粒子特效精准时间
    /// </summary>
    /// <returns></returns>
    public static float ParticleSystemLength(Transform game)
    {
        var pts = game.GetComponentsInChildren<ParticleSystem>();
        float maxDuration = 0f;
        foreach(var p in pts)
        {
            if(p.enableEmission)
            {
                if(p.loop)
                {
                    return -1f;
                }
                float dunration = 0f;
                if(p.emissionRate <= 0)
                {
                    dunration = p.startDelay + p.startLifetime;
                }
                else
                {
                    dunration = p.startDelay + Mathf.Max(p.duration, p.startLifetime);
                }
                if (dunration > maxDuration) 
                {
                    maxDuration = dunration;
                }
            }
        }
        return maxDuration;
    }
}
