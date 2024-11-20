using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerBattleState : EnemyState
{
    private Transform player;
    private CarnivorousFlower flower;
    public CarnivorousFlowerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, CarnivorousFlower _flower) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.flower = _flower;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }
    public override void Update()
    {
        base.Update();

        if (flower.IsPlayerDetectedFL())
        {
            stateTimer = flower.BattleTime;

            if (flower.IsPlayerDetectedFL().distance < flower.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(flower.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, flower.transform.position) > 2f)
                stateMachine.ChangeState(flower.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    private bool CanAttack()
    {
        if (Time.time >= flower.lastTimeAttacked + flower.attackCooldown)
        {
            flower.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

}
