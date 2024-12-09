using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class S_Patrol : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float randomTime;
    float randomAttack;
    float timer;

    public S_Patrol(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    public void Enter()
    {
       
        timer = 0;
        randomTime = Random.Range(3f, 6f);
        randomAttack = Random.Range(0f, 100f);
    }


    public void Update()
    {
        timer += Time.deltaTime;

        if (botControl_dattt.IsTarget)
        {
            botControl_dattt.ChangeDirection(PlayerControl.Instance.transform.position.x > botControl_dattt.transform.position.x);

            if (botControl_dattt.BotType == BotType.NightBone)
            {
                Exit();
                botControl_dattt.ChangeTele();
            }

            if (botControl_dattt.IsTargetInAttackRange())
            {
                Exit();
                botControl_dattt.ChangeAttack();
            }
            else if (botControl_dattt.IsTargetInRangeAttackRange())
            {
                if (botControl_dattt.BotType == BotType.GolemBoss)
                {
                    if (randomAttack < 50f)
                    {
                        Exit();
                        botControl_dattt.ChangeRangeAttack();
                    }
                    else
                    {
                        Exit();
                        botControl_dattt.ChangeLaserAttack();
                    }
                }
                else
                {
                    Exit();
                    botControl_dattt.ChangeRangeAttack();
                }
            }
            else
            {
                botControl_dattt.Moving();
                botControl_dattt.Anim.Run();
            }

        }
        else
        {
            if (timer < randomTime)
            {
                botControl_dattt.Moving();
                botControl_dattt.Anim.Run();

            }
            else
            {
                Exit();
                botControl_dattt.ChangeIdle();
            }
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_Patrol)));
    }
}
