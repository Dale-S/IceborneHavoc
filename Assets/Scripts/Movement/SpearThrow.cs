using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    [Header("References")] 
    public GameObject spear;
    public Transform spearStart;
    private Rigidbody rb;
    public Transform bodyTransform;
    public ParticleSystem sigil;
    public AudioSource charge;
    
    
    [Header("Key Mapping")]
    public KeyCode spearKey = KeyCode.E;

    [Header("Variables")] 
    private static int numOfSpears = 3;
    private IList<GameObject> spears = new GameObject[numOfSpears];
    private int totalSpears = 0;
    public float spearCoolDown;
    private float cd = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cd > 0)
        {
            cd -= 1 * Time.deltaTime;
        }
        if (Input.GetKeyDown(spearKey))
        {
            if (cd <= 0)
            {
                sigil.Play();
                charge.Play();
                Invoke(nameof(shootSpear),1f);
                cd = spearCoolDown;
            }
        }
    }

    private void shootSpear()
    {
        totalSpears++;
        int spot = totalSpears % 3;
        Quaternion tempRot = Quaternion.LookRotation(spearStart.forward);
        GameObject temp = Instantiate(spear, spearStart.position, tempRot);
        GameObject tempSpear = spears[spot];
        spears[spot] = temp;
        tempSpear.GetComponent<SpearForward>().spearEffect();
    }
}

