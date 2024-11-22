using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackState : EnemyState
{
    private Golem golem;

    public GolemAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName,Golem _golem) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.golem = _golem;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        golem.lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();
        golem.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(golem.battleState);
        }
    }
}
