using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public int moveCycles;
    private void Awake()
    {
        moveForward();
    }

    private void Update()
    {
        
    }

    private void turn()
    {
        float thisTurn = Random.RandomRange(0, 360);
        this.gameObject.transform.rotation = Quaternion.Euler(0f, thisTurn, 0f);
    }
    private void moveForward()
    {
        for (int i = 0; i < moveCycles; i++)
        {
            rb.AddForce(this.transform.forward.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }
}
