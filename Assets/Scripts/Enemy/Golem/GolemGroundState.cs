using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemGroundState : EnemyState
{
    // GolemGroundState có những cái thêm khác với EnemyState
    // từ GolemGroundState kế thừa từ EnemyState vvà thêm các phần khác 
    protected Golem golem;

    protected Transform player;
    public GolemGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Golem _golem) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.golem = _golem;
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
        // battleState là trạng thái chiến đấu 
        base.Update();
        if (golem.IsPlayerDetected() || Vector2.Distance(golem.transform.position, player.position) < 2)
        {
            stateMachine.ChangeState(golem.battleState);
        }
    }
}
