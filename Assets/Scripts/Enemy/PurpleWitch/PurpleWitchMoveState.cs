using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchMoveState : PurpleWitchGroundState
{
    public PurpleWitchMoveState(PurpleWitch _purpleWitch, EnemyStateMachine _stateMachine, string _animBoolName, PurpleWitch purpleWitch) : base(_purpleWitch, _stateMachine, _animBoolName, purpleWitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // khi khai báo song phải khởi tạo ở đây
        purpleWitch.SetVelocity(purpleWitch.moveSpeed * purpleWitch.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        var purpleWitchPos = purpleWitch.transform.position.x;

        if (purpleWitchPos > purpleWitch.right)
        {
            //  Debug.Log("max");
            purpleWitch.SetVelocity(-purpleWitch.moveSpeed, rb.velocity.y);

        }
        else if (purpleWitchPos < purpleWitch.left)
        {

            purpleWitch.SetVelocity(purpleWitch.moveSpeed, rb.velocity.y);


        }
        


        // muốn chạy code thì phải update lên 

    }
}
