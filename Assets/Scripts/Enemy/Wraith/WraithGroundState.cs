using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithGroundState : EnemyState
{
    protected Wraith_Enemy wraith;
    protected Transform player;

    public WraithGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Wraith_Enemy _wraith) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.wraith = _wraith;
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
        if (wraith.IsPlayerDetected() || Vector2.Distance(wraith.transform.position, player.position) < 10)
        {
            stateMachine.ChangeState(wraith.battleState);
        }
    }
}
