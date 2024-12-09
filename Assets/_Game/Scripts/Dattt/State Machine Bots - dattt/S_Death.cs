using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Death : IStateNormal
{
    BotControl_dattt botControl_dattt;
    public S_Death(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    private void Death()
    {
        if (botControl_dattt.BotType == BotType.FlyingEye)
        {
            botControl_dattt.Rb.gravityScale = 1;
        }

        botControl_dattt.IsDeath = true;
        botControl_dattt.StopMoving();
        botControl_dattt.Anim.Death();

        botControl_dattt.StateMachine.ActiveStates.Clear();
    }

    public void Enter()
    {
        Debug.Log("Enter Death");
        Death();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Death)));
    }
}
