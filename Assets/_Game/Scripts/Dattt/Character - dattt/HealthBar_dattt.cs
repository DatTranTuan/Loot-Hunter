using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_dattt : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(float currentHealth, float maxHealth) 
    {
        healthBar.value = currentHealth / maxHealth;
    } 
}
