using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Idle : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Idle(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void Enter()
    {
        Debug.Log("Enter P_Idle");

        playerControl.Anim.Idle();
        playerControl.IsJumping = false;

        if (playerControl.IsGrounded)
        {
            playerControl.Rb.velocity = Vector2.zero;
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
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Idle)));
    }
}
