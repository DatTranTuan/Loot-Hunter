using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Shield : IStateNormal
{
    BotControl_dattt botControl_dattt;


    public S_Shield(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter S_Shield");
        botControl_dattt.StopMoving();
        botControl_dattt.IsImune = true;
        botControl_dattt.Anim.Shield();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Shield)));
    }
}
