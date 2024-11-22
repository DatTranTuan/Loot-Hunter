using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAttackState : EnemyState
{
    private Wraith_Enemy wraith;
    public WraithAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Wraith_Enemy _wraith) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.wraith = _wraith;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        wraith.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        wraith.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(wraith.battleState);
        }
    }
}
