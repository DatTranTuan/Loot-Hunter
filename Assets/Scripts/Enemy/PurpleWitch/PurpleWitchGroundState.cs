using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchGroundState : EnemyState
{
    protected PurpleWitch purpleWitch;
    protected Transform player;
    public PurpleWitchGroundState(PurpleWitch _purpleWitch, EnemyStateMachine _stateMachine, string _animBoolName, PurpleWitch purpleWitch) : base(_purpleWitch, _stateMachine, _animBoolName)
    {
        this.purpleWitch = purpleWitch;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (purpleWitch.IsPlayerDetected() || Vector2.Distance(purpleWitch.transform.position, player.position) < 10)
        {
            stateMachine.ChangeState(purpleWitch.battleState);
        }
    }
}
