using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMiniGroundState : EnemyState
{
    protected WraithMini wraithMini;
    protected Transform player;
    public WraithMiniGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, WraithMini _wraithMini) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.wraithMini = _wraithMini;
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
        if(wraithMini.IsPlayerDetected() ||Vector2.Distance(wraithMini.transform.position, player.position) < 10)
        {
            stateMachine.ChangeState(wraithMini.battleState);
        }
    }
}
