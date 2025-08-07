using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float breakAngle;

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x,
                                         this.transform.position.y,
                                         goal.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(direction),
                                                   Time.deltaTime * rotSpeed);

        if(Vector3.Angle(goal.forward,this.transform.forward) > breakAngle && speed > 2)
        {
            speed = Mathf.Clamp(speed - (decceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
