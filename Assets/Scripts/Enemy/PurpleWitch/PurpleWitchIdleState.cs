using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchIdleState : PurpleWitchGroundState
{
    public PurpleWitchIdleState(PurpleWitch _purpleWitch, EnemyStateMachine _stateMachine, string _animBoolName, PurpleWitch purpleWitch) : base(_purpleWitch, _stateMachine, _animBoolName, purpleWitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = purpleWitch.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(purpleWitch.moveState);
        }
    }
}

