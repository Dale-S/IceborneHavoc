using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyTracking : MonoBehaviour
{
    public GameObject[] EnemySpawners;
    private int total;
    public int left;

    private void Start()
    {
        foreach (var e in EnemySpawners)
        {
            total += e.GetComponent<SpawnerScript>().numOfEnemies;
        }

        left = total;
    }

    private void Update()
    {
        if (left <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
