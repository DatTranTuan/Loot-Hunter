using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PS_Moving : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Moving(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void DoubleExit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Jump)));
        Exit();
    }

    public void Enter()
    {
        Debug.Log("Enter P_Moving");
    }

    public void Update()
    {
        if (Mathf.Abs(playerControl.Horizontal) > 0.01f)
        {
            if (playerControl.IsGrounded && !playerControl.IsJumping)
            {
                playerControl.Anim.Run();
            }

            playerControl.Rb.velocity = new Vector2(playerControl.Horizontal * playerControl.Speed, playerControl.Rb.velocity.y);
            playerControl.transform.rotation = Quaternion.Euler(new Vector3(0, playerControl.Horizontal > 0 ? 0 : 180, 0));

            if (playerControl.PStateMachine.ActivePStates.Contains(playerControl.PStateMachine.GetState(typeof(PS_Jump))))
            {
                playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Jump)));
            }

            if (!playerControl.IsJumping && !playerControl.IsGrounded )
            {
                playerControl.Anim.JumpFall();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && !playerControl.IsJumping && playerControl.IsGrounded)
            {
                //Exit();
                DoubleExit();
                playerControl.ChangeJumping();
            }

            if (Input.GetKeyDown(KeyCode.C) && !playerControl.IsJumping && playerControl.IsGrounded)
            {
                //Exit();
                DoubleExit();
                playerControl.ChangeRoll();
            }

            if (Input.GetKeyDown(KeyCode.X) && !playerControl.IsJumping && playerControl.IsGrounded)
            {
                //Exit();
                DoubleExit();
                playerControl.ChangeAttack();
            }
        }
        else if (Mathf.Abs(playerControl.Horizontal) < 0.01f && !playerControl.IsRolling)
        {
            if (playerControl.IsGrounded && !playerControl.IsAttack && !playerControl.IsRolling)
            {
                Exit();
                playerControl.ChangeIdle();
            }

            if (!playerControl.IsGrounded && !playerControl.IsJumping)
            {
                playerControl.Anim.JumpFall();
            }
        }

    }

    public void Exit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Moving)));
    }
}
