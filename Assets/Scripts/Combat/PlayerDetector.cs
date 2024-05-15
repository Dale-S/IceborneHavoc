using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public EnemyMovement EM;
    public EnemyAttack EA;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EM.setChase(true);
            EA.ChaseDelay();
        }
    }
}
