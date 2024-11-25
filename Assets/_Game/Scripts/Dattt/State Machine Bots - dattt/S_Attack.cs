using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Attack : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float timer;

    float randomShield;

    public S_Attack(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
        Debug.Log("Enter Attack");

        botControl_dattt.IsImune = false;

        if (botControl_dattt.IsTarget)
        {
            if (botControl_dattt.BotType != BotType.Skeleton)
            {
                botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);
                botControl_dattt.StopMoving();

                //botControl_dattt.Attack();
                botControl_dattt.Anim.Attack();
            }
            else
            {
                randomShield = Random.Range(0f, 100f);

                if (randomShield <= 50f)
                {
                    Exit();
                    botControl_dattt.ChangeShield();
                }
                else
                {
                    botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);
                    botControl_dattt.StopMoving();

                    //botControl_dattt.Attack();
                    botControl_dattt.Anim.Attack();
                }
            }
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
