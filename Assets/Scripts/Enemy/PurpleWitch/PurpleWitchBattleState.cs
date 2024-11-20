using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchBattleState : EnemyState
{
    private PurpleWitch purpleWitch;
    private Transform player;
    private int moveDir;
    public PurpleWitchBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, PurpleWitch _purpleWitch) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.purpleWitch = _purpleWitch;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
      //  Debug.Log("Im in battle state");
    }


    public override void Update()
    {
        base.Update();

        Vector2 direction = (player.position - purpleWitch.transform.position).normalized;

        if (purpleWitch.IsPlayerDetected())
        {
            stateTimer = purpleWitch.BattleTime;
            if(purpleWitch.IsPlayerDetected().distance < purpleWitch.attackDistance )
            {
                if(CanAttack())
                    stateMachine.ChangeState(purpleWitch.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, purpleWitch.transform.position) > 7)
                stateMachine.ChangeState(purpleWitch.idleState);
        }

        float desiredHeight = player.position.y + 1.5f;

        Vector3 newPosition = purpleWitch.transform.position;
        newPosition.y = Mathf.Max(newPosition.y, desiredHeight); // Chọn giá trị cao nhất giữa độ cao hiện tại và độ cao mong muốn
        purpleWitch.transform.position = newPosition;

        if (direction.x > 0)
            moveDir = 1;
        else if (direction.x < 0)
            moveDir = -1;

        purpleWitch.SetVelocity(purpleWitch.moveSpeed * direction.x, purpleWitch.moveSpeed * direction.y);

    }
    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= purpleWitch.lastTimeAttacked + purpleWitch.attackCooldown)
        {
            purpleWitch.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

}
