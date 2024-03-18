using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    int currentHealth;
    public int maxHealth = 100;


    void Start()
    {
        currentHealth = maxHealth;
    }

  public void TakeDamage (int damage)
  {
    currentHealth -= damage;

    if(currentHealth <=0)
    {
        Die();
    }


    void Die() 
    {
      Debug.Log("enemy dead");
      Destroy(gameObject);
    }

  }

}
