using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float baseDamage = 10f;
    public KeyCode attackKey = KeyCode.Mouse0;
    public PlayerMovement PM;
    public Camera cam;

    private void Update()
    {
        if(Input.GetKeyDown(attackKey))
        {
            attack();
        }
    }

    private void attack()
    {
        Debug.Log("Attacked");
        RaycastHit hit;
        if (Physics.BoxCast(gameObject.transform.position, new Vector3(1f, 1f, 1f), cam.transform.forward, out hit, gameObject.transform.rotation, 2f))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyHealth>().dealDamage(baseDamage * PM.currSpeed);
                Debug.Log("Enemy Hit");
            }
        }
    }
}
