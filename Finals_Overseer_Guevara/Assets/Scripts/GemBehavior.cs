using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    public float rotSpeed = 100f;
    public Transform player;
    public float speed = 9f;
    public float rotationSpeed = 5f;

    public bool playerObtained = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player == null)
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }
        else
        {
            Vector3 lookAtPlayer = new Vector3(player.position.x,
                                           this.transform.position.y,
                                           player.position.z);

            Vector3 direction = lookAtPlayer - transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(lookAtPlayer, transform.position) > 1.5)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("ThePlayer") && !playerObtained)
        {
            player = other.gameObject.transform;
            playerObtained = true;
            PlayerStats stats = player.GetComponent<PlayerStats>();

            if(stats != null)
            {
                stats.GemObtained();
            }
        }
    }
}
