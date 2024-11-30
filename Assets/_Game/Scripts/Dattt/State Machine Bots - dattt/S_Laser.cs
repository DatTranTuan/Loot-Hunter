using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Laser : IStateNormal
{
    BotControl_dattt botControl_dattt;

    public S_Laser(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter LaserAttack");

        if(botControl_dattt.IsTarget)
        {
            botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);
            botControl_dattt.StopMoving();
            botControl_dattt.LaserAttack();
        }
        else
        {
            Exit();
            botControl_dattt.ChangeIdle();
        }
    }

    public void Update()
    {
        if (!botControl_dattt.IsTarget)
        {
            Exit();
            botControl_dattt.ChangePatrol();
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Laser)));
    }
}
