using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerAttackState : EnemyState
{
    private CarnivorousFlower flower;
    public CarnivorousFlowerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, CarnivorousFlower _flower) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.flower = _flower;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        flower.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(flower.battleState);
        }
    }
}
