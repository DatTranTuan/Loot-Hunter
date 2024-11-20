using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStats : CharacterStats
{

    private Golem golem;
    protected override void Start()
    {
        base.Start();
        golem = GetComponent<Golem>();  
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        golem.Damage();
    }

    public override void Die()
    {
        base.Die();
        golem.Die();
    //    Destroy(gameObject, .4f);
    }
}
