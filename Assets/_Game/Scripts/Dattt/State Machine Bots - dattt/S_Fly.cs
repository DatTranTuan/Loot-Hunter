using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class S_Fly : IStateNormal
{
    BotControl_dattt botControl_dattt;

    public S_Fly(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        if (botControl_dattt.CurrentHealth <= 0)
        {
            Exit();
            botControl_dattt.ChangeDeath();
        }

        botControl_dattt.Anim.Fly();
        botControl_dattt.StartFly();
    }

    public void Update()
    {
        if (!botControl_dattt.IsFlying)
        {
            Exit();
            botControl_dattt.ChangeIdle();
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Fly)));
    }
}
