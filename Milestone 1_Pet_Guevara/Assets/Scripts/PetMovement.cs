using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float rotSpeed;

    public void LateUpdate()
    {
        Vector3 lookAtPlayer = new Vector3(player.position.x,
                                           this.transform.position.y,
                                           player.position.z);

        Vector3 direction = lookAtPlayer - transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(direction),
                                                   rotSpeed * Time.deltaTime);

        if(Vector3.Distance(lookAtPlayer, transform.position) > 1.5)
        {
            transform.Translate(0,0,speed * Time.deltaTime);
        }
    }
}
