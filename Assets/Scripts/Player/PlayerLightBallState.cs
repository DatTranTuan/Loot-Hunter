using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightBallState : PlayerGroundState
{
    private float elapsedTime = 0f;
    private bool waiting = false;

    public PlayerLightBallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0f;
        waiting = true;
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //    stateMachine.ChangeState(player.idleState);
        // Nếu đang chờ và thời gian đã đủ 1 giây
        if (waiting && elapsedTime >= 0.5f)
        {
            stateMachine.ChangeState(player.idleState);
            waiting = false;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
