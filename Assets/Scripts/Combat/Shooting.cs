using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject spearPrefab;
    public Transform spearSpawnPoint;
    public float spearSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
            var spear = Instantiate(spearPrefab, spearSpawnPoint.position, spearSpawnPoint.rotation);
            spear.GetComponent<Rigidbody>().velocity = spearSpawnPoint.forward * spearSpeed;
    }


 
}
