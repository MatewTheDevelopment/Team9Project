using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour, IDamageblePlayer
{
    public int Health;

    public void Update()
    {
        HealtPlayerCheck();
    }

    public void HealtPlayerCheck()
    {
        if (Health <= 0)
        {
            Death();
        }

        if(Health >= 100)
        {
            Health = 100;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
    }
}
