using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{
    [SerializeField] private float zomMaxHealth;
    [SerializeField] private HealthBar_dattt healthBar;

    private float zomCurrentHealth;

    private void Awake()
    {
        zomCurrentHealth = zomMaxHealth;
        healthBar = GetComponentInChildren<HealthBar_dattt>();
    }

    public void TakeDamage()
    {
        zomCurrentHealth -= 20f;
        healthBar.UpdateHealthBar(zomCurrentHealth, zomMaxHealth);
    }
}
