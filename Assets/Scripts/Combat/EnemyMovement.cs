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
    public int chaseCycles = 1;
    private float moveTimer;
    public float moveTime;
    private Transform spawnPoint;
    private float distance;
    public float maxDistance = 30f;
    private bool inChase = false;
    private bool turning = false;
    private Vector3 flatVel;
    private int i;
    private Transform player;
    
    private void Awake()
    {
        moveForward();
        moveTimer = moveTime;
        player = GameObject.Find("PlayerCombined").gameObject.transform.GetChild(1);
    }

    private void Update()
    {
        RaycastHit hit;
        distance = Vector3.Distance(transform.position, spawnPoint.transform.position);
        if (moveTimer <= 0 && !inChase)
        {
            if (distance >= maxDistance)
            {
                Vector3 from = new Vector3(transform.position.x, 2f, transform.position.z);
                Vector3 to = new Vector3(spawnPoint.transform.position.x, 2f, spawnPoint.transform.position.z);
                Quaternion lookTo = Quaternion.LookRotation((to - from).normalized);
                rb.MoveRotation(lookTo);
                Invoke(nameof(moveForward), 0.5f);
                moveTimer = moveTime;
            }
            else
            {
                turnRand();
                turning = false;
                checkForWall();
                if (!Physics.Raycast(transform.position, transform.forward, 5f))
                {
                    Invoke(nameof(moveForward), 0.5f);
                    i = 0;
                    moveTimer = moveTime;
                }
            }
        }
        else if (inChase && moveTimer <= 0)
        {
            Vector3 from = new Vector3(transform.position.x, 2f, transform.position.z);
            Vector3 to = new Vector3(player.position.x, 2f, player.position.z);
            Quaternion lookTo = Quaternion.LookRotation((to - from).normalized);
            rb.MoveRotation(lookTo);
            Invoke(nameof(moveForwardChase), 0.25f);
            moveTimer = 1f;
        }
        
        moveTimer -= 1 * Time.deltaTime;
    }

    private void turnRand()
    {
        float thisTurn = Random.RandomRange(-90, 91);
        transform.Rotate(0, transform.rotation.y + thisTurn, 0);
    }
    private void moveForward()
    {
        for (int i = 0; i < moveCycles; i++)
        {
            rb.AddForce(transform.forward.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    private void turn45()
    {
        transform.Rotate(0,45,0);
    }

    public void setSpawn(Transform spawn)
    {
        spawnPoint = spawn;
    }
    
    private void moveBackward()
    {
        for (int i = 0; i < moveCycles; i++)
        {
            rb.AddForce((-transform.forward.normalized) * moveSpeed * 10f, ForceMode.Force);
        }
    }

    private void checkForWall()
    {
        if(Physics.Raycast(transform.position, transform.forward, 5f))
        {
            turning = true;
            if (i >= 2)
            {
                moveBackward();
            }
            turn45();
            i++;
            moveTimer = 0.25f;
        }
    }

    public void setChase(bool state)
    {
        inChase = state;
        Debug.Log($"Currently In Chase: {inChase}");
    }

    private void moveForwardChase()
    {
        rb.velocity = new Vector3(0, 0, 0);
        for (int i = 0; i < chaseCycles; i++)
        {
            rb.AddForce(transform.forward.normalized * moveSpeed * 1f, ForceMode.VelocityChange);
        }
    }
}
