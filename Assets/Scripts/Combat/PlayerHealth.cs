using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    private bool invincible;
    public float damageCooldown = 2f;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            Destroy(gameObject);
        }

        if (timer <= 0)
        {
            invincible = false;
        }

        timer -= 1 * Time.deltaTime;
    }

    public void damagePlayer()
    {
        if (!invincible)
        {
            playerHealth--;
            invincible = true;
            timer = damageCooldown;
        }
    }
}
