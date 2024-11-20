using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Attack : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float timer;

    public S_Attack(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter Attack");

        if (botControl_dattt.IsTarget)
        {
            botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);
            botControl_dattt.StopMoving();
            
            //botControl_dattt.Attack();
            botControl_dattt.Anim.Attack();
        }

        timer = 0;
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            Exit();
            botControl_dattt.ChangePatrol();
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Attack)));
    }

}
