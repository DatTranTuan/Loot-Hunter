using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Death : IStatePlayer
{
    PlayerControl playerControl;

    public PS_Death(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    public void Enter()
    {
        SoundManager.Instance.Play("PDeath");

        playerControl.PStateMachine.ActivePStates.Clear();
        playerControl.Rb.velocity = Vector2.zero;
        playerControl.IsDeath = true;
        playerControl.Anim.Death();

        UIManager.Instance.DeathPanel.SetActive(true);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
