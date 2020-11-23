using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]private Transform plane;
    private Vector3 savePosition;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        savePosition = transform.position;
    }

    void Update()
    {
        if ((int)Mathf.Abs((transform.position - plane.position).y) > 20f)
        {
            transform.position = savePosition;
        }
    }
    /// <summary>
    /// 跳
    /// </summary>
    /// <param name="size">大小</param>
    public void Jump(float size=0)
    {
        if ((int)Mathf.Abs((transform.position - plane.position).y) > 0.5f)
        {
            return;
        }
        if (size <= 0)
            size = 5;
        Debug.Log($"{transform.name}");
        _rigidbody.AddForce(Vector3.up*size);
    }
    /// <summary>
    /// 跑
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="orientation">方向</param>
    public void Run(float speed,Vector3 orientation)
    {
        transform.Translate(orientation * speed * Time.deltaTime);
    }
}
