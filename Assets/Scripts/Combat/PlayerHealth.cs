using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
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
    public GameObject[] Lives;
    public GameObject UI;
    

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
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
            Lives[playerHealth].SetActive(false);
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

    public void killPlayer()
    {
        playerHealth = 0;
        foreach (var i in Lives)
        {
          i.SetActive(false);  
        }
    }
}
