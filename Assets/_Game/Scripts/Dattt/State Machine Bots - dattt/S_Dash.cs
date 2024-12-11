using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class S_Dash : IStateNormal
{
    BotControl_dattt botControl_dattt;
    public S_Dash(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        botControl_dattt.Anim.Dash();

        Vector3 directionBehindPlayer = -PlayerControl.Instance.transform.right;

        Vector3 backPosition = PlayerControl.Instance.transform.position + directionBehindPlayer * - 1.5f;
        botControl_dattt.transform.position = backPosition;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Dash)));
    }
}
