using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;

    public void Start()
    {
        Camera cam = Camera.main;
    }
    public void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        transform.position = Vector3.Slerp(transform.position, newPos, speed * Time.deltaTime);

    }
}
