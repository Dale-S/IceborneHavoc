using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public int numOfEnemies = 0;
    public GameObject[] enemies;
    private int currEnemies;
    public int[] numOfType;
    public Transform enemySpawn;
    private bool enemySpawned = false;
    private float timeToSpawn = 4;
    private float timer = 0;
    private int i = 0;
    private int numToSpawn = 0;
    private GameObject currentEnemy;
    private bool nEnemy = true;

    private void Start()
    {
        currEnemies = numOfEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (currEnemies <= 0)
        {
            return;
        }
        
        if (timer <= 0)
        {
            if (nEnemy)
            {
                numToSpawn = numOfType[i];
                nEnemy = false;
            }
            currentEnemy = Instantiate(enemies[i], enemySpawn.position, enemySpawn.rotation);
            currentEnemy.GetComponent<EnemyMovement>().setSpawn(enemySpawn);
            if (numToSpawn <= 0)
            {
                i++;
                nEnemy = true;
            }
            timer = timeToSpawn;
            numToSpawn--;
            currEnemies--;
        }
        
        timer -= 1 * Time.deltaTime;
    }
}
