using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RangeAttack : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float count = 0;
    float randomValue;

    public S_RangeAttack(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter RangeAttack");

        randomValue = Random.Range(0f, 100f);

        if (botControl_dattt.IsTarget)
        {
            botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);
            botControl_dattt.StopMoving();

            if (count <= 1)
            {
                botControl_dattt.Anim.RangeAttack();
                count++;
            }
            else
            {
                if (randomValue <= 25f)
                {
                    botControl_dattt.Anim.RangeAttack();
                }
                else
                {
                    count = 0;
                    Exit();
                    botControl_dattt.ChangeIdle();
                }
            }
        }
        else
        {
            Exit();
            botControl_dattt.ChangePatrol();
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
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_RangeAttack)));
    }
}
