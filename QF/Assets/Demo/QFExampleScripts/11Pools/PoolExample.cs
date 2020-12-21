using System;
using System.Collections;
using QFramework;
using UnityEngine;
public class PoolExample : MonoBehaviour
{
    private SimpleObjectPool<GameObject> fishPool;
    private SimpleObjectPool<GameObject> cartoonyPunchExtremefishPool;
    private GameObject cartoonyPunchExtremeParent;
    private Transform parent;
    private GameObject bullet;
    private GameObject cartoonyPunchExtreme;
    private GameObject wall;
    private GameObject outGame;
    private float time;
    private bool isFitstDown=true;
    private Vector3 dir;
    private GameObject playerBlood;
    private bool isControl=true;
    private Transform[] hidden;
    private float invincible = 3f;//无敌时间
    private float allTheTime;
    private bool isW;
    private bool isS;
    private GameObject enemy;
    private bool isWall;
    
    [SerializeField][Range(0,5)] private int enemyCount;
    [SerializeField] private float speed=15;
    [SerializeField] private float rotateSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        hidden=transform.GetComponentsInChildren<Transform>();
        cartoonyPunchExtremeParent=new GameObject("CartoonyPunchExtremes");
        cartoonyPunchExtremeParent.transform.position = Vector3.zero;
        bullet = Resources.Load<GameObject>("11Pools/Bullet");
        enemy = Resources.Load<GameObject>("11Pools/Enemy");
        playerBlood = Instantiate(Resources.Load<GameObject>("11Pools/PlayerBlood"));
        playerBlood.GetComponent<ParticleSystem>().playOnAwake = false;
        playerBlood.SetActive(false);
        cartoonyPunchExtreme = Resources.Load<GameObject>("11Pools/CartoonyPunchExtreme");
        parent = transform.Find("Head");
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy);
        }
        CreatWall();
        fishPool=CraetPools(() =>
        {
            GameObject game = Instantiate(bullet, parent);
            game.SetActive(false);
            return game;
        },1);
        cartoonyPunchExtremefishPool=CraetPools(() =>
        {
            GameObject game = Instantiate(cartoonyPunchExtreme, cartoonyPunchExtremeParent.transform);
            game.SetActive(false);
            return game;
        },1);
    }
    void Update()
    {
        if (isControl)
        {
            KeyControls();
        }
    }
    void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            Move(dir.normalized);
        }
    }
    /// <summary>
    /// 按键操作
    /// </summary>
    private void KeyControls()
    {
        if (Input.GetKey(KeyCode.K))
        {
            if (isFitstDown)
            {
                isFitstDown = false;
                Launch();
            }
            else
            {
                time += Time.deltaTime;
                if (time > 0.15)
                {
                    time = 0;
                    Launch();   
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            time = 0;
            isFitstDown = true;
        }
        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += -transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*Time.deltaTime*rotateSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*Time.deltaTime*rotateSpeed);
        }
    }
    /// <summary>
    /// 判断是否超出坐标
    /// </summary>
    /// <returns></returns>
    private bool GetPosion(Vector3 vector3)
    {
        Vector3 screenPoint=Camera.main.WorldToScreenPoint(vector3);
        float maX = Screen.width - 150;
        float minX =150;
        float maY = Screen.height - 150;
        float minY =150;
        if (screenPoint.x > minX && screenPoint.x < maX && screenPoint.y > minY && screenPoint.y < maY)
            return true;
        return false;
    }
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="dir">方向</param>
    private void Move(Vector3 dir)
    {
        Vector3 AboutToMove = transform.position + Time.deltaTime * dir * speed;
        if (GetPosion(AboutToMove))
        {
            isWall=false;
            transform.position = AboutToMove;
        }
        else
        {
            isWall=true;
            Die();
        }
    }
    /// <summary>
    /// 发射
    /// </summary>
    private void Launch()
    {
        outGame=fishPool.Allocate();
        outGame.SetActive(true);
        if (outGame.GetComponent<BulletControls>().fishPool == null)
        {
            outGame.GetComponent<BulletControls>().fishPool = fishPool;
            outGame.GetComponent<BulletControls>().cartoonyPunchExtremefishPool = cartoonyPunchExtremefishPool;
        }
        outGame.GetComponent<BulletControls>().isLaunch=true;
    }

    /// <summary>
    /// 初始化内存池
    /// </summary>
    /// <param name="factoryMethod"></param>
    /// <param name="count">池内数量</param>
    /// <param name="resetMethod"></param>
    private SimpleObjectPool<T> CraetPools<T>(Func<T> factoryMethod, int count, Action<T> resetMethod = null)
    {
       return new SimpleObjectPool<T>(factoryMethod, resetMethod, count);
    }
    /// <summary>
    /// 创建围墙
    /// </summary>
    void CreatWall()
    {
        GameObject walls=new GameObject("Walls");
        walls.transform.position = Vector3.zero;
        Vector3 oriCoord = Vector3.zero;
        wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.tag = "Walls";
        wall.transform.parent = walls.transform;
        //左
        oriCoord=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-Screen.width,Screen.height/2,0));
        oriCoord.z = 0;
        wall.transform.position = oriCoord;
        wall.transform.localScale=new Vector3(1,Screen.height,1);
        //右
        GameObject LeftWall=Instantiate(wall,walls.transform);
        LeftWall.transform.position = new Vector3(-oriCoord.x,oriCoord.y,0);
        LeftWall.transform.localScale=new Vector3(1,Screen.height,1);
        //上
        oriCoord=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height,0));
        oriCoord.z = 0;
        GameObject UptWall=Instantiate(wall,walls.transform);
        UptWall.transform.position = oriCoord;
        UptWall.transform.localScale=new Vector3(Screen.width,1,1);
        //下
        GameObject DowntWall=Instantiate(wall,walls.transform);
        DowntWall.transform.position = new Vector3(oriCoord.x,-oriCoord.y,0);
        DowntWall.transform.localScale=new Vector3(Screen.width,1,1);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        if (allTheTime == 0&&isControl)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            isControl = false;
            for (int i = 1; i < hidden.Length; i++)
            {
                hidden[i].gameObject.SetActive(false);
            }
            playerBlood.SetActive(true);
            playerBlood.transform.position = transform.position;
            playerBlood.GetComponent<ParticleSystem>().Play();
            StartCoroutine(SelfResurgence());
        }
    }
    /// <summary>
    /// 自动复活
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelfResurgence()
    {
        yield return new WaitForSeconds(Tools.ParticleSystemLength(playerBlood.transform)+3);
        Resurgence();
    }
    /// <summary>
    /// 复活
    /// </summary>
    public void Resurgence()
    {
        playerBlood.GetComponent<ParticleSystem>().Pause();
        playerBlood.SetActive(false);
        for (int i = 1; i < hidden.Length; i++)
        {
            hidden[i].gameObject.SetActive(true);
        }
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isControl = true;
        StartCoroutine(Invincible());
    }
    /// <summary>
    /// 无敌时间
    /// </summary>
    /// <returns></returns>
    private IEnumerator Invincible()
    {
        allTheTime = invincible;
        while (true)
        {
            yield return new WaitForSeconds(1);
            allTheTime--;
            if (allTheTime == 0)
            {
                transform.GetComponent<BoxCollider>().enabled = true;
                if (isWall)
                {
                    isWall=false;
                    Die();
                }
                break;
            }
        }
    }
}
