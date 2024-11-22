using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMiniIdleState : WraithMiniGroundState
{

    public WraithMiniIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, WraithMini _wraithMini) : base(_enemyBase, _stateMachine, _animBollName, _wraithMini)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = wraithMini.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(wraithMini.wraithMiniMove);
        }
    }
}
