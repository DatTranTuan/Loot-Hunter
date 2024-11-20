using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimationTrigger : MonoBehaviour
{
    private Golem golem => GetComponentInParent<Golem>();

    private void AnimationTrigger()
    {
        golem.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        // dung 1 chuoi de check dc nhieu enemy [] // OverlapCircleAll vong tron 

        // golem.attackCkeck.positionvi tri cua cuong tron
        //  golem.attackCkeckRadius ban kinh cua vong tron 
        Collider2D[] collider2s = Physics2D.OverlapCircleAll(golem.attackCkeck.position, golem.attackCkeckRadius);

        // check xem co bao nhieu collider o trong vong tron
        foreach(var hit in collider2s)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
                hit.GetComponent<CharacterStats>().TakeDamage(golem.stats.damage);
            }
        }
    }
}
