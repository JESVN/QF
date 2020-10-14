using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoad : MonoBehaviour
{
    private float speed = 200;
    // Update is called once per frame
    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}
