using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Wait : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float randomTime;
    float timer;

    public S_Wait(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle");
        botControl_dattt.StopMoving();
        botControl_dattt.Anim.Idle();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            Exit();
            botControl_dattt.ChangePatrol();
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Wait)));
    }
}
