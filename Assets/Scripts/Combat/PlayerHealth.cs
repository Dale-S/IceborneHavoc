using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    private bool invincible;
    public float damageCooldown = 2f;
    private float timer;
    public bool dead;
    public Animator screen;
    public GameObject GameOverScreen;
    private GameObject[] Enemies;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            GameOverScreen.SetActive(true);
            dead = true;
            screen.Play("GameOverAnimation");
            deleteEnemies();
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

    private void deleteEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in Enemies)
        {
            Destroy(e);
        }
    }
}
