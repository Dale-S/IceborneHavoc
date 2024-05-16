using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth PH;
    public float attackTimer = 3f;
    private float timer;

    private void Start()
    {
        PH = GameObject.Find("PlayerCombined").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timer <= 0)
        {
            PH.damagePlayer();
            timer = attackTimer;
        }
    }

    private void Update()
    {
        timer -= 1 * Time.deltaTime;
    }

    public void ChaseDelay()
    {
        timer = attackTimer;
    }
}
