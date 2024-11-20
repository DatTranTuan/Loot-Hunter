using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMoveState : WraithGroundState
{
    public WraithMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Wraith_Enemy _wraith) : base(_enemyBase, _stateMachine, _animBollName, _wraith)
    {
        this.wraith = _wraith;
    }

    public override void Enter()
    {
        base.Enter();
        wraith.SetVelocity(wraith.moveSpeed * wraith.facingDir, wraith.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        var WraithWitchPos = wraith.transform.position.x;
        if (WraithWitchPos > wraith.right)
        {
          //  Debug.Log("max");
            wraith.SetVelocity(-wraith.moveSpeed, rb.velocity.y);

        }
        else if (WraithWitchPos < wraith.left)
        {
            wraith.SetVelocity(wraith.moveSpeed, rb.velocity.y);


        }
    }
}
