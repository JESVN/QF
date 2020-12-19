using QFramework;
using UnityEngine;
using System.Collections;
public class CartoonyPunchExtremeControls : MonoBehaviour
{
    public SimpleObjectPool<GameObject> cartoonyPunchExtremefishPool;
    private ParticleSystem particleSystem;
    private float duration;
    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var particleSystemMain = particleSystem.main;
        particleSystemMain.playOnAwake = false;
        duration=Tools.ParticleSystemLength(transform);
    }
    public void Play()
    {
        particleSystem.Play();
        StartCoroutine(IEHidden());
    }
    private IEnumerator IEHidden()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        cartoonyPunchExtremefishPool.Recycle(gameObject);
    }
}
