using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TakeHit : IStateNormal
{
    BotControl_dattt botControl_dattt;

    float timer;

    public S_TakeHit(BotControl_dattt botControl_dattt)
    {
        this.botControl_dattt = botControl_dattt;
    }

    private void TakeHit()
    {
        botControl_dattt.IsTakeDmg = true;
        botControl_dattt.DamageTaken = 50;

        if (botControl_dattt.IsTakeDmg)
        {
            //Debug.Log("Bot Take Dmgggg");
            botControl_dattt.CurrentHealth -= botControl_dattt.DamageTaken;

            if (botControl_dattt.CurrentHealth <= 0)
            {
                Exit();
                botControl_dattt.ChangeDeath();
            }

            botControl_dattt.IsTakeDmg = false;
        }
    }


    public void Enter()
    {
        Debug.Log("Enter TakeHit");
        botControl_dattt.Anim.TakeHit();
        TakeHit();
    }


    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.75f)
        {
            Exit();
            botControl_dattt.ChangePatrol();
        }
    }

    public void Exit()
    {
        botControl_dattt.StateMachine.Exit(botControl_dattt.StateMachine.GetState(typeof(S_TakeHit)));
    }
}
