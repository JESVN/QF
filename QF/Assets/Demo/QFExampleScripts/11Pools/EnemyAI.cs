using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    private enum  AIState
    {
        自动,
        锁定
    }
    private enum  WS
    {
        W,
        Stop,
    }
    private enum  AD
    {
        A,
        D,
    }

    [SerializeField] private AIState aiState = AIState.自动;
    [SerializeField] private float speed=10;
    [SerializeField] private float rotateSpeed=10;
    private Vector3 dir;
    private WS _ws=WS.W;
    private AD _ad=AD.D;
    private float time;
    private float randomTime;
    private float dirTime;
    private float dirRandomTime;
    private float playerTime;
    private Transform[] hidden;
    private GameObject[] players;
    private Transform player;
    private bool isMove = true;
    private GameObject evemtBlood;
    private bool isDie=false;
    private WS ws
    {
        get
        {
            return _ws;
        }
        set
        {
            _ws = value;
            SetMove();
        }
    }
    private AD ad
    {
        get
        {
            return _ad;
        }
        set
        {
            _ad = value;
            SetDir();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        hidden = transform.GetComponentsInChildren<Transform>();
        evemtBlood = Instantiate(Resources.Load<GameObject>("11Pools/EnemyBlood"));
        evemtBlood.GetComponent<ParticleSystem>().playOnAwake = false;
        evemtBlood.SetActive(false);
        PosionPointReset();
        ResetData();
        //Debug.Log($"{player.name}");
    }
    // Update is called once per frame
    void Update()
    {
        if (isMove&&!isDie)
        {
            if (aiState.Equals(AIState.自动))
            {
                dir = Vector3.zero;
                RandomHandle();
                ToRandomHandle();
            }
            else
            {
                transform.LookAt(player);
                transform.position=Vector3.MoveTowards(transform.position,player.position,1f*Time.deltaTime);
                playerTime+=Time.deltaTime;
                if (playerTime > 15f)
                {
                    RandomHandlePlayer();
                    playerTime = 0;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (isMove&&!isDie)
        {
            if (aiState.Equals(AIState.自动))
            {
                if (dir != Vector3.zero)
                {
                    Move(dir.normalized);
                }
            }
        }
    }
    /// <summary>
    /// 判断是否超出坐标
    /// </summary>
    /// <returns></returns>
    private bool GetPosion()
    {
        Vector3 screenPoint=Camera.main.WorldToScreenPoint(transform.position);
        float maX = Screen.width - 100;
        float minX =100;
        float maY = Screen.height - 100;
        float minY =100;
        if (screenPoint.x >= maX || screenPoint.x <= minX || screenPoint.y >= maY || screenPoint.y <= minY)
            return false;
        return true;
    }
    /// <summary>
    /// 随机锁定目标
    /// </summary>
    private void RandomHandlePlayer()
    {
        int index=Random.Range(0, players.Length);
        player = players[index].transform; 
    }
    /// <summary>
    /// 转向判断
    /// </summary>
    private void ToRandomHandle()
    {
        dirTime += Time.deltaTime;
        if (dirTime >= dirRandomTime)
        {
            SetRandomDir();
            SetRandomTimeTo();
            dirTime = 0;
        }
        else
        {
            SetDir();
        }
    }
    /// <summary>
    /// 移动判断
    /// </summary>
    private void RandomHandle()
    {
        time += Time.deltaTime;
        if (time >= randomTime)
        {
            SetRandomDMove();
            SetRandomTime();
            time = 0;
        }
        else
        {
            SetMove();
        }
    }
    /// <summary>
    /// 设置方向随机
    /// </summary>
    private void SetRandomDir()
    {
        float number=Random.Range(1,100);
        if (number < 50)
        {
            dirRandomTime = 0.3f;
        }
        else if (number>=25 && number<50)
        {
            dirRandomTime = 0.6f;
        }
        else if (number>=50 && number<75)
        {
            dirRandomTime = 0.9f;
        }
        else if (number>=75 && number<100)
        {
            dirRandomTime = 1.2f;
        }
    }
    /// <summary>
    /// 设置移动随机
    /// </summary>
    private void SetRandomDMove()
    {
        float number=Random.Range(1,100);
        if (number < 50)
        {
            randomTime = 0.5f;
        }
        else if (number>=25 && number<50)
        {
            randomTime = 1.5f;
        }
        else if (number>=50 && number<75)
        {
            randomTime = 2f;
        }
        else if (number>=75 && number<100)
        {
            randomTime = 3f;
        }
    }
    /// <summary>
    /// 设置移动变化
    /// </summary>
    private void SetRandomTime()
    {
        float number=Random.Range(1,100);
        if (number <15)
        {
            ws = WS.Stop;
        }
        else
        {
            ws = WS.W;
        }
    }
    /// <summary>
    /// 设置转向变化
    /// </summary>
    private void SetRandomTimeTo()
    {
        float number=Random.Range(1,100);
        if (number < 50)
        {
            ad = AD.A;
        }
        else
        {
            ad = AD.D;
        }
    }
    /// <summary>
    /// 设置移动
    /// </summary>
    private void SetMove()
    {
        switch (ws)
        {
            case WS.W:
                dir += Vector3.forward;
                break;
            case WS.Stop:
                dir = Vector3.zero;
                break;
        }
    }
    /// <summary>
    /// 设置方向
    /// </summary>
    private void SetDir()
    {
        switch (ad)
        {
            case AD.A:
                transform.Rotate(Vector3.up*Time.deltaTime*rotateSpeed);
                break;
            case AD.D:
                transform.Rotate(Vector3.down*Time.deltaTime*rotateSpeed);
                break;
        }
    }
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="dir">方向</param>
    private void Move(Vector3 dir)
    {
        transform.Translate(Time.deltaTime*dir*speed);  
    }
    /// <summary>
    /// 碰撞检测进入
    /// </summary>
    /// <param name="collider"></param>
    void OnCollisionEnter(Collision collider)
    {
        switch (collider.transform.tag)
        {
            case "Player":
                if (!isDie)
                {
                    collider.transform.GetComponent<PoolExample>().Die();
                }
                break;
            case "Walls":
                if (!isDie)
                {
                    dir = Vector3.zero;
                }
                break;
        }
    }
    /// <summary>
    /// 碰撞检测持续
    /// </summary>
    /// <param name="collider"></param>
    void OnCollisionStay(Collision collider)
    {
        switch (collider.transform.tag)
        {
            case "Player":
                if (!isDie)
                {
                    collider.transform.GetComponent<PoolExample>().Die();
                }
                break;
            case "Walls":
                if (!isDie)
                {
                    dir = Vector3.zero;
                }
                break;
        }
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public bool Die()
    {
        if (!isDie)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            isDie = true;
            for (int i = 1; i < hidden.Length; i++)
            {
                hidden[i].gameObject.SetActive(false);
            }
            evemtBlood.SetActive(true);
            evemtBlood.transform.position = transform.position;
            evemtBlood.GetComponent<ParticleSystem>().Play();
            StartCoroutine(SelfResurgence());
            return true;
        }
        return false;
    }
    /// <summary>
    /// 自动复活
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelfResurgence()
    {
        yield return new WaitForSeconds(Tools.ParticleSystemLength(evemtBlood.transform)+2.5f);
        Resurgence();
    }
    /// <summary>
    /// 复活
    /// </summary>
    public void Resurgence()
    {
        evemtBlood.GetComponent<ParticleSystem>().Pause();
        evemtBlood.SetActive(false);
        for (int i = 1; i < hidden.Length; i++)
        {
            hidden[i].gameObject.SetActive(true);
        }
        PosionPointReset();
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        isDie = false;
        transform.GetComponent<BoxCollider>().enabled = true;
        ResetData();
    }
    /// <summary>
    /// 重置
    /// </summary>
    private void ResetData()
    {
        int rom=Random.Range(0, 2);
        if (rom == 0)
            aiState = AIState.锁定;
        else
            aiState = AIState.自动;
        RandomHandlePlayer();
        SetRandomTimeTo();
    }
    /// <summary>
    /// 坐标重置
    /// </summary>
    private void PosionPointReset()
    {
        int x=Random.Range(300, Screen.width-300);
        int y=Random.Range(300, Screen.height-300);
        Vector3 posion=Camera.main.ScreenToWorldPoint(new Vector3(x,y,0));
        posion.z = 0;
        transform.position = posion;
    }
}
