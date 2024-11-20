using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMiniMoveState : WraithMiniGroundState
{

    public WraithMiniMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, WraithMini _wraithMini) : base(_enemyBase, _stateMachine, _animBollName, _wraithMini)
    {

    }

    public override void Enter()
    {
        base.Enter();
        wraithMini.SetVelocity(wraithMini.moveSpeed * wraithMini.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        var wraithMiniPos = wraithMini.transform.position.x;
        if(wraithMiniPos> wraithMini.right)
        {
            wraithMini.SetVelocity(-wraithMini.moveSpeed, rb.velocity.y);
        }
        else if (wraithMiniPos < wraithMini.left)
        {
            wraithMini.SetVelocity(wraithMini.moveSpeed, rb.velocity.y);
        }
    }
}
