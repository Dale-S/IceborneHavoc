using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
public LayerMask enemyLayers;
public Transform attackPoint;
public GameObject IceHammer;
//public Animator animator;
public bool CanAttack = true;
public bool IsAttacking = false;
public float AttackCooldown = 1.0f;
public float attackRange = 0.5f;
public int attackDamage = 20;

//public AudioClip HammerSound;

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

    //Audio Code
    //AudioSource ac = GetComponent<AudioSource>();
    //ac.PlayOneShot(HammerSound);


    //Animation Code
    //animator.SetTrigger("Attack")

    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

foreach (Collider enemy in hitEnemies)
{
    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    Debug.Log("hit");
}




    StartCoroutine(ResetAttackCooldown());




}

void OnDrawGizmosSelected()
{
    if(attackPoint == null)
        return;

    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
