using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHP = 100f;

    public void TakeDamage(float damage)
    {
        float finalDamage = Mathf.Clamp(damage, 0, currentHP);
        currentHP -= finalDamage;

        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
