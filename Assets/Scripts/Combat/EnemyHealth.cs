using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 20;

    private void Update()
    {
        if (Health <= 0)
        {
            //Play animation or effect here
            Invoke(nameof(destroyEnemy), 0.15f);
        }
    }

    private void destroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void dealDamage(float damage)
    {
        Health -= damage;
    }
}
