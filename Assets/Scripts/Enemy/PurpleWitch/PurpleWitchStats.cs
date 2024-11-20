using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchStats : CharacterStats
{
    private PurpleWitch witch;
    protected override void Start()
    {
        base.Start();
        witch = GetComponent<PurpleWitch>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        witch.Damage();
    }

    public override void Die()
    {
        base.Die();
        witch.Die();
    }

}
