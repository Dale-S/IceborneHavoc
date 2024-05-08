using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;
    public int numOfEnemies = 0;
    public int[] numOfType;
    public Transform enemySpawn;
    private bool enemySpawned = false;

    // Update is called once per frame
    void Update()
    {
        
    }
}
