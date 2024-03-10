using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
public GameObject IceHammer;
public bool CanAttack = true;
public bool IsAttacking = false;
public float AttackCooldown = 1.0f;

public AudioClip HammerSound;

 void Update()
 {
    if(Input.GetMouseButtonDown(0))
    {
        if(CanAttack)
        {
            HammerAttack();
        }
    }
 }

public void HammerAttack()
{
    CanAttack = false;
    IsAttacking = true;
    AudioSource ac = GetComponent<AudioSource>();
    ac.PlayOneShot(HammerSound);


    //Animation Code



    StartCoroutine(ResetAttackCooldown());


}

IEnumerator ResetAttackCooldown()
{
    StartCoroutine(ResetAttackBool());
    yield return new WaitForSeconds(AttackCooldown);
    CanAttack = true;
}

IEnumerator ResetAttackBool()
{
    yield return new WaitForSeconds(1.0f);
    IsAttacking = false;
}

}
