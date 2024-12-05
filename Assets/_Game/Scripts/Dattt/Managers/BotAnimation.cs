using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private String currentAnimName;

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void Attack()
    {
        ChangeAnim("Attack");
    }

    public void RangeAttack()
    {
        ChangeAnim("Range Attack");
    }

    public void Idle()
    {
        ChangeAnim("Idle");
    }

    public void Run()
    {
        ChangeAnim("Run");
    }

    public void Guard()
    {
        ChangeAnim("Guard");
    }

    public void Shield()
    {
        ChangeAnim("Shield");
    }

    public void Tele()
    {
        ChangeAnim("Tele");
    }

    public void Dash()
    {
        ChangeAnim("Dash");
    }

    public void TakeHit()
    {
        ChangeAnim("Take Hit");
    }

    public void Death()
    {
        ChangeAnim("Death");
    }
}
