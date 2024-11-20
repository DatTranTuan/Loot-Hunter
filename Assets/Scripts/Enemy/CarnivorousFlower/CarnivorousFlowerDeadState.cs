using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerDeadState : EnemyState
{
    private CarnivorousFlower flower;

    public CarnivorousFlowerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, CarnivorousFlower _flower) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.flower = _flower;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        flower.SetZeroVelocity();
    }
}
