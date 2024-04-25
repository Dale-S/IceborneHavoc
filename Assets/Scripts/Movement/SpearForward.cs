using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class SpearForward : MonoBehaviour
{
    //Variables 
    private Rigidbody rb;
    private bool snapped = false;
    public ParticleSystem hitEffect;
    
    [Header("Spear Settings")] 
    public float spearSpeed;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.BoxCast(gameObject.transform.position, new Vector3(0.002f, 0.002f, 0.002f), gameObject.transform.forward, out hit, gameObject.transform.rotation, 2.25f))
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            if (snapped == false)
            {
                this.gameObject.transform.rotation = Quaternion.LookRotation(-hit.normal);
                snapped = true;
                this.gameObject.transform.GetChild(0).tag = "Spear";
                this.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
                this.gameObject.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                hitEffect.Play();
            }
        }
        else
        {
            rb.velocity = gameObject.transform.forward * spearSpeed;
        }
    }

    //Make function for playing animation
}
