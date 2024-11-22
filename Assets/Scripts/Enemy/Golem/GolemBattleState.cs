using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBattleState : EnemyState
{
    // khai bao bien lay vi tri nguoi choi
    private Transform player;
    // khai bao enemy
    private Golem golem;
    private int moveDir;

    public GolemBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName,Golem _golem) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.golem = _golem;
    }

    public override void Enter()
    {
        base.Enter();
        // tim gameobject co ten la Player trong unity
        player = GameObject.Find("Player").transform;
       // Debug.Log("Im in battle state");
    }
    public override void Update()
    {
        base.Update();

        if (golem.IsPlayerDetected())
        {
            stateTimer = golem.BattleTime;

            if (golem.IsPlayerDetected().distance < golem.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(golem.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, golem.transform.position) > 7)
                stateMachine.ChangeState(golem.idleState);
        }


        if (player.position.x > golem.transform.position.x)
            moveDir = 1;
        else if (player.position.x < golem.transform.position.x)
            moveDir = -1;

        golem.SetVelocity(golem.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool CanAttack()
    {
        if (Time.time >= golem.lastTimeAttacked + golem.attackCooldown)
        {
            golem.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
