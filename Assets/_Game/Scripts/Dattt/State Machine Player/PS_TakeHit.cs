using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_TakeHit : IStatePlayer
{
    PlayerControl playerControl;

    public PS_TakeHit(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void Enter()
    {
        if (playerControl.CurrentHealth <= 0)
        {
            Exit();
            playerControl.ChangeDeath();
            
        }
    }

    public void Update()
    {
        if (playerControl.IsGrounded && !playerControl.IsJumping && !playerControl.IsRolling && !playerControl.IsAttack)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Exit();
                playerControl.ChangeJumping();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Exit();
                playerControl.ChangeAttack();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Exit();
                playerControl.ChangeRoll();
            }
        }

        if (Mathf.Abs(playerControl.Horizontal) > 0.1f && !playerControl.IsRolling)
        {
            Exit();
            playerControl.ChangeMoving();
        }
    }

    public void Exit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_TakeHit)));
    }
}
