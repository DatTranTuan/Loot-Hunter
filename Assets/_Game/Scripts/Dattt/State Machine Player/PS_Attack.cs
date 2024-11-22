using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Attack : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Attack(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }


    public void Enter()
    {
        Debug.Log("Enter P_Attack");

        if (!playerControl.IsAttack)
        {
            playerControl.Anim.Attack();
            //playerControl.Attack();
        }
    }

    public void Update()
    {
        //if (!playerControl.IsRolling && playerControl.IsGrounded && !playerControl.IsAttack)
        //{
        //    Exit();
        //    playerControl.ChangeIdle();
        //}
        //else
        if (Input.GetKeyDown(KeyCode.C) && !playerControl.IsAttack)
        {
            Exit();
            playerControl.ChangeRoll();
        }
    }

    public void Exit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Attack)));
    }
}
