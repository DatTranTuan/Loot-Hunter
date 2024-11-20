using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerGroundState : EnemyState
{
    protected CarnivorousFlower carnivorousFlower;
    protected Transform player;


    public CarnivorousFlowerGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, CarnivorousFlower carnivorousFlower) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.carnivorousFlower = carnivorousFlower;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;

    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (carnivorousFlower.IsPlayerDetectedFL())
        {
            stateMachine.ChangeState(carnivorousFlower.battleState);
        }
    }
}
