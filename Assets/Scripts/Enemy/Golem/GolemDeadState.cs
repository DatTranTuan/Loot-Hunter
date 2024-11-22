using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDeadState : EnemyState
{
    private Golem golem;
    public GolemDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName,Golem _golem) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.golem = _golem;
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
        golem.SetZeroVelocity();
    }
}
