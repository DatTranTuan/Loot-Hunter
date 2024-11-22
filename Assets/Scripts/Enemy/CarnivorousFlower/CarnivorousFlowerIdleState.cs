using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerIdleState : EnemyState
{
    protected CarnivorousFlower carnivorous;
    protected Transform player;

    public CarnivorousFlowerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName,CarnivorousFlower _carnivorous) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.carnivorous = _carnivorous;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        player = GameObject.Find("Player").transform;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (carnivorous.IsPlayerDetectedFL())
        {
            stateMachine.ChangeState(carnivorous.battleState);
        }
    }
}
