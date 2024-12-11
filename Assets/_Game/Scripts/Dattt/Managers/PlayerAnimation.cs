using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
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

    public void Roll()
    {
        ChangeAnim("Roll");
    }

    public void Jump()
    {
        ChangeAnim("Jump");
    }

    public void JumpFall()
    {
        ChangeAnim("JumpFall");
    }

    public void Attack()
    {
        ChangeAnim("Attack");
    }

    public void Idle()
    {
        ChangeAnim("Idle");
    }

    public void Run()
    {
        ChangeAnim("Run");
    }

    public void TakeHit()
    {
        ChangeAnim("Take Hit");
    }

    public void Death()
    {
        ChangeAnim("DeathNoMove");
    }
}
