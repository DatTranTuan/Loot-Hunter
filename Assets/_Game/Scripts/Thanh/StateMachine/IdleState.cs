using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float ramdomTime;
    float timer;
    public void OnEnter(Enemy_Zombie enemy)
    {
        enemy.StopMoving();
        timer = 0;
        ramdomTime = Random.Range(1f, 2f);
    }

    public void OnExcute(Enemy_Zombie enemy)
    {
        timer += Time.deltaTime;
        if (timer > ramdomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
       
    }

    public void OnExit(Enemy_Zombie enemy)
    {
        
    }

  
}
