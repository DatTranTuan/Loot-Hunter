using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PS_Roll : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Roll(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void Roll()
    {
        playerControl.IsAttack = false;
        playerControl.Rb.velocity = Vector2.zero;
        playerControl.Rb.AddForce(new Vector2(playerControl.transform.right.x * playerControl.RollForce, 0));

        playerControl.IsImune = true;
        playerControl.IsRolling = true;
    }

    public void Enter()
    {
        Debug.Log("Enter P_Roll");
        playerControl.Anim.Roll();
        Roll();
    }

    public void Update()
    {
        if (!playerControl.IsRolling && playerControl.IsGrounded && !playerControl.IsImune)
        {
            Exit();
            playerControl.ChangeIdle();
        }
    }

    public void Exit()
    {
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Roll)));
    }
}
