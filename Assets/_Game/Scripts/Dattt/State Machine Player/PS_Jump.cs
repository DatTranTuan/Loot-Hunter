using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Jump : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Jump(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void Jump()
    {
        playerControl.IsJumping = true;
        playerControl.Rb.AddForce(playerControl.JumpForce * Vector2.up);
    }

    public void Enter()
    {
        Debug.Log("Enter P_Jump");
        playerControl.Anim.Jump();
        Jump();
    }

    public void Update()
    {
        if (playerControl.IsJumping && Mathf.Abs(playerControl.Horizontal) > 0.01f)
        {
            playerControl.ChangeMoving();
        }
        else if (playerControl.Rb.velocity.y < 0 && !playerControl.IsJumping)
        {
            playerControl.Anim.JumpFall();

            if (Mathf.Abs(playerControl.Horizontal) > 0.01f)
            {
                playerControl.Anim.JumpFall();
                playerControl.ChangeMoving();
            }

            if (playerControl.IsGrounded)
            {
                Exit();
                playerControl.ChangeIdle();
            }
        }
    }

    public void Exit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Jump)));
    }
}
