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
    public bool inAttack = false; // For animation
    public AudioSource hammerSound;
    private bool attacking = false; // For damage dealing

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
        Invoke(nameof(backToIdle), 0.85f);
        inAttack = true;
        attacking = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Enemy") && attacking)
        {
            if (PM.currSpeed >= 20)
            {
                headAnim.Play("break");
                breakEffect.Play();
                Invoke(nameof(backToHeadIdle), 1f);
            }
            other.transform.GetComponent<EnemyHealth>().dealDamage(baseDamage + (baseDamage * (PM.currSpeed / 10)));
            attacking = false;
        }
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
