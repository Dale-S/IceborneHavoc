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

    private void Start()
    {
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
            //Play animation or effect here
            Invoke(nameof(destroyEnemy), 0.15f);
        }
    }

    private void destroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void dealDamage(float damage)
    {
        Health -= damage;
    }
}
