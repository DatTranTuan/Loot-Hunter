using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float ramdomTime;
    float timer;
    public void OnEnter(Enemy_Zombie enemy)
    {
        timer = 0;
        ramdomTime = Random.Range(2f, 4f);
    }

    public void OnExcute(Enemy_Zombie enemy)
    {
        timer += Time.deltaTime;

        if(enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
           
            
        }
        else
        {
            if (timer < ramdomTime)
            {
                
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Enemy_Zombie enemy)
    {

    }
}
