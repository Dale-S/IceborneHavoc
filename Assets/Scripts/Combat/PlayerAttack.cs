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
    public Animator hammerAnim;
    public Animator headAnim;
    public float attackCD = 1f;
    private float CDtimer = 0f;
    public ParticleSystem breakEffect;
    public bool inAttack = false;
    public AudioSource hammerSound;

    private void Update()
    {
        if(Input.GetKeyDown(attackKey))
        {
            if (CDtimer <= 0)
            {
                attack();
                CDtimer = attackCD;
            }
        }
        CDtimer -= 1 * Time.deltaTime;
    }

    private void attack()
    {
        hammerAnim.Play("Swing");
        hammerSound.Play();
        RaycastHit hit;
        if (Physics.BoxCast(gameObject.transform.position, new Vector3(5.5f, 1.5f, 1.5f), cam.transform.forward, out hit, gameObject.transform.rotation, 5f))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                if (PM.currSpeed >= 20)
                {
                    headAnim.Play("break");
                    breakEffect.Play();
                    Invoke(nameof(backToHeadIdle), 1f);
                }
                hit.transform.GetComponent<EnemyHealth>().dealDamage(baseDamage + (baseDamage * (PM.currSpeed / 10)));
            }
        }
        Invoke(nameof(backToIdle), 0.85f);
        inAttack = true;
    }
    
    private void backToIdle()
    {
        hammerAnim.Play("Idle");
        inAttack = false;
    }
    private void backToHeadIdle()
    {
        headAnim.Play("headIdle");
    }
}
