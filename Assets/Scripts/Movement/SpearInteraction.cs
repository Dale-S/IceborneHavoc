using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearInteraction : MonoBehaviour
{
    private Rigidbody rb;
    
    [Header("Spear Movement Properties")] 
    public float spearSwingHeight = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected from player");
        if (other.gameObject.CompareTag("Spear"))
        {
            other.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
            Vector3 currSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.velocity = new Vector3(rb.velocity.x, spearSwingHeight, rb.velocity.z);
        }
    }
}
