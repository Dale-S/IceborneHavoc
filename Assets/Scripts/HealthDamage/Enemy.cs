using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 100;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, whatIsPlayer;

    //patroling around
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
      player = GameObject.Find("Player").transform;
      agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      //checking sight and attack range
      playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

      if(!playerInSightRange && !playerInAttackRange) Patroling();
      if(playerInSightRange && !playerInAttackRange) ChasePlayer();
      if(playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patroling()
    {
      if (!walkPointSet) SearchWalkPoint();

      if (walkPointSet)
        agent.SetDestination(walkPoint);
    
    Vector3 distanceToWalkPoint = transform.position - walkPoint;

      //Walkpoint reached
      if (distanceToWalkPoint.magnitude < 1f)
          walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
      //random point in range

      float randomZ = Random.Range(-walkPointRange, walkPointRange);
      float randomX = Random.Range(-walkPointRange, walkPointRange);

      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

      if(Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
      agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
      //make enemy stay still when attacking
      agent.SetDestination(transform.position);

      transform.LookAt(player);

      if(!alreadyAttacked)
      {
        //attack code goes here

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
      }
    }

  //visualize attack on sight range
  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, attackRange);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, sightRange);
  }

    private void ResetAttack()
    {
      alreadyAttacked = false;
    }


    void Start()
    {
        currentHealth = maxHealth;
    }

  public void TakeDamage (int damage)
  {
    currentHealth -= damage;

    if(currentHealth <=0)
    {
        Die();
    }


    void Die() 
    {
      Debug.Log("enemy dead");
      Destroy(gameObject);
    }

  }

}
