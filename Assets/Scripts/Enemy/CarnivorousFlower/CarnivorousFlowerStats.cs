using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerStart : CharacterStats
{
    private CarnivorousFlower flower;

    protected override void Start()
    {
        base.Start();
        flower = GetComponent<CarnivorousFlower>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        flower.Damage();
    }

    
    public override void Die()
    {
        base.Die();
        flower.Die();
    }
}
