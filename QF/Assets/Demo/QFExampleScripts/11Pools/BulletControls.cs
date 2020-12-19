using QFramework;
using UnityEngine;
public class BulletControls : MonoBehaviour
{
    public float speed = 30;
    public bool isLaunch;
    public SimpleObjectPool<GameObject> fishPool;
    public SimpleObjectPool<GameObject> cartoonyPunchExtremefishPool;
    void Update()
    {
        if (isLaunch)
        {
            transform.localPosition += Vector3.right * speed * Time.deltaTime;
        }
    }
    /// <summary>
    /// 触发检测
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.parent!=null)
        {
            if (collider.transform.tag.Equals("Walls"))
            {
                Debug.Log($"{"打到墙壁了"}");
                GameObject game=cartoonyPunchExtremefishPool.Allocate();
                game.SetActive(true);
                if (game.GetComponent<CartoonyPunchExtremeControls>().cartoonyPunchExtremefishPool == null)
                {
                    game.GetComponent<CartoonyPunchExtremeControls>().cartoonyPunchExtremefishPool =
                        cartoonyPunchExtremefishPool;
                }
                game.transform.position = transform.position;
                game.GetComponent<CartoonyPunchExtremeControls>().Play();
                isLaunch = false;
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
                fishPool.Recycle(gameObject);
            }
        }
        else
        {
            if (collider.transform.tag.Equals("Enemy"))
            {
                if (collider.transform.GetComponent<EnemyAI>().Die())
                {
                    transform.localPosition = Vector3.zero;
                    gameObject.SetActive(false);
                    fishPool.Recycle(gameObject);
                    Debug.Log($"打到敌人了");  
                }  
            }   
        }
    }
}
