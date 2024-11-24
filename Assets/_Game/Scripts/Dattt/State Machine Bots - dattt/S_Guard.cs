using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Guard : IStateNormal
{
    BotControl_dattt botControl_dattt;

    public S_Guard(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter Guard");
        botControl_dattt.Anim.Guard();
        botControl_dattt.IsImune = true;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Guard)));
    }
}
