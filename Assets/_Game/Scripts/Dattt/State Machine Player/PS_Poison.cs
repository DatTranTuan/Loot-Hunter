using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Poison : IStatePlayer
{
    PlayerControl playerControl;

    private int index = 0;
    private float timer = 0f;
    private float delay = 1f;

    public PS_Poison(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void PoisonDmg(float dmg)
    {
        playerControl.CurrentHealth -= dmg;
        playerControl.HealthBar.UpdateHealthBar(playerControl.CurrentHealth, playerControl.MaxHealth);
    }

    public void Enter()
    {
        Debug.Log("Enter P_Poison");

        if (playerControl.CurrentHealth <= 0)
        {
            Exit();
            playerControl.ChangeDeath();
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (index < 4)
        {
            if (timer >= delay)
            {
                index++;
                PoisonDmg(10f);
                timer = 0f;
            }
        }
        else
        {
            Exit();
            playerControl.ChangeIdle();
        }

        if (playerControl.IsGrounded && !playerControl.IsJumping && !playerControl.IsRolling && !playerControl.IsAttack)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerControl.ChangeJumping();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                playerControl.ChangeAttack();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                playerControl.ChangeRoll();
            }
        }

        if (Mathf.Abs(playerControl.Horizontal) > 0.1f && !playerControl.IsRolling)
        {
            playerControl.ChangeMoving();
        }
    }

    public void Exit()
    {
        index = 0;
        playerControl.PStateMachine.Exit(playerControl.PStateMachine.GetState(typeof(PS_Poison)));
    }
}
