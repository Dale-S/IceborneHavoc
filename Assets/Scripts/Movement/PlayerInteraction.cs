using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpearDetector"))
        {
            Debug.Log("Collided with SpearDetector");
            Invoke(nameof(enableCollider), 0.2f);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpearDetector"))
        {
            Debug.Log("Collided with SpearDetector");
            Invoke(nameof(enableCollider), 0.25f);
        }
    }

    private void enableCollider()
    {
        this.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
    }
}
