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
        playerControl.Anim.Death();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
