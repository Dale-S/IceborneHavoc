using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 20;
    private float MaxHealth;
    public Slider slider;
    public TextMeshProUGUI healthText;
    private float timer = 0;
    private float damageDebounce = 0.25f;
    public ParticleSystem bone;
    public MeshRenderer EnemyModel;
    public GameObject HealthBar;
    public BoxCollider EnemyCollider;
    public bool dead = false;
    private GameObject tracker;

    private void Start()
    {
        tracker = GameObject.Find("bonewall");
        MaxHealth = Health;
        slider.maxValue = Health;
        healthText.text = $"{Mathf.RoundToInt(Health)} / {Mathf.RoundToInt(MaxHealth)}";
    }

    private void Update()
    {
        healthText.text = $"{Mathf.RoundToInt(Health)} / {Mathf.RoundToInt(MaxHealth)}";
        slider.value = Health;
        if (Health <= 0)
        {
            if (!dead)
            {
                tracker.GetComponent<EnemyTracking>().left--;
                EnemyModel.enabled = false;
                EnemyCollider.enabled = false;
                HealthBar.SetActive(false);
                bone.Play();
                dead = true;
            }
            Invoke(nameof(destroyEnemy), 01.5f);
        }

        timer -= 1 * Time.deltaTime;
    }

    private void destroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void dealDamage(float damage)
    {
        if (timer <= 0)
        {
            Health -= damage;
            timer = damageDebounce;
        }
    }
}
