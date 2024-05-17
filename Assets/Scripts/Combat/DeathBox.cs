using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("PlayerCombined");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Body")
        {
            player.GetComponent<PlayerHealth>().killPlayer();
        }
    }
}
