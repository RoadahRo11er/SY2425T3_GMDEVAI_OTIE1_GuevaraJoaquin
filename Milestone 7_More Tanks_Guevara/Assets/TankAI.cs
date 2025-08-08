using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Health health;
    Animator anim;
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;

    public GameObject GetPlayer()
    {
        if (player != null)
            { return player; }
        else { return null; }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        health = this.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        }
        else
        {
            anim.SetFloat("distance", 100f);
        }
        anim.SetFloat("hp",health.currentHP);
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }
}
