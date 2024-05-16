using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class SpearForward : MonoBehaviour
{
    //Variables 
    private Rigidbody rb;
    private bool snapped = false;
    public ParticleSystem hitEffect;
    public ParticleSystem breakEffect;
    public AudioSource breakSound;
    private bool destroyed = false;
    public GameObject spearMesh;
    public ParticleSystem[] particles;
    
    
    [Header("Spear Settings")] 
    public float spearSpeed;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (destroyed)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.BoxCast(gameObject.transform.position, new Vector3(0.002f, 0.002f, 0.002f), gameObject.transform.forward, out hit, gameObject.transform.rotation, 2.25f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyHealth>().dealDamage(10f);
                spearEffect();
                return;
            }
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
    public void spearEffect()
    {
        if (!destroyed)
        {
            gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
            breakEffect.Play();
            breakSound.Play();
            rb.velocity = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            spearMesh.SetActive(false);
            Destroy(particles[0]);
            Destroy(particles[1]);
            destroyed = true;
            Invoke(nameof(destroySpear), 0.75f);
        }
    }

    private void destroySpear()
    {
        Destroy(gameObject);
    }
}
