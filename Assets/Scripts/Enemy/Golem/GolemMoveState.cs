using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GolemMoveState : GolemGroundState
{
    public GolemMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Golem _golem) : base(_enemyBase, _stateMachine, _animBollName, _golem)
    {
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
        golem.SetVelocity(golem.moveSpeed * golem.facingDir, rb.velocity.y);
        if (golem.IsWallDetected() || !golem.IsGroundDetected())
        {
            golem.Flip();
            stateMachine.ChangeState(golem.idleState);
        }
        /*
        var golemPos = golem.transform.position.x;
        golem.SetVelocity(golem.moveSpeed * golem.facingDir, rb.velocity.y);

        if (golemPos > golem.right)
        {
            Debug.Log("golemmax");
            golem.SetVelocity(-golem.moveSpeed, rb.velocity.y);

        }
        else if (golemPos < golem.left)
        {
            golem.SetVelocity(golem.moveSpeed, rb.velocity.y);


        }
        */
    }
}
