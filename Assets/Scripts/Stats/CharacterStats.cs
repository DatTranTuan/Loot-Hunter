using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //  dam gay ra
    public int damage;

    public System.Action onHealthChanged;
    // HP toi da
    public int maxHealth;
    // HP hien tai
    public  int currentHealth;

    protected virtual void Start()
    {
        // khoi tao HP t
        currentHealth = maxHealth;
    }

    // tạo hàm tính dam gây ra và HP
    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }
    public virtual void healing(int _hp)
    {
        currentHealth += _hp;
        
    }

    // tạo hàm sử lý  su kien khi hết HP
    public virtual void Die()
    {
   //     throw new NotImplementedException();
    }
}
