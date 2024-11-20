using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchAttackState : EnemyState
{
    private PurpleWitch purpleWitch;
    public PurpleWitchAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, PurpleWitch _purpleWitch) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.purpleWitch = _purpleWitch;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        purpleWitch.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        purpleWitch.SetZeroVelocity();
        if(triggerCalled)
            stateMachine.ChangeState(purpleWitch.battleState);
    }
}
