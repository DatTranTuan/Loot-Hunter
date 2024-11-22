using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithStats : CharacterStats
{
    private Wraith_Enemy wraith;
    protected override void Start()
    {
        base.Start();
        wraith = GetComponent<Wraith_Enemy>();

    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        wraith.Damage();
    }

    public override void Die()
    {
        base.Die();
        wraith.Die();
    }
}
