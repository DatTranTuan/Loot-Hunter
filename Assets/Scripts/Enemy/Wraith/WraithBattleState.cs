using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBattleState : EnemyState
{
    // khai bao bien lay vi tri nguoi choi
    private Transform player;
    // khai bao enemy
    private Wraith_Enemy wraith;
    private int moveDir;

  
  
    public WraithBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Wraith_Enemy _wraith) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.wraith = _wraith;
    }

    public override void Enter()
    {
        base.Enter();
        // tim gameobject co ten la Player trong unity
        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        Vector2 direction = (player.position - wraith.transform.position).normalized;
        if (wraith.IsPlayerDetected())
        {
            stateTimer = wraith.BattleTime;

            if (wraith.IsPlayerDetected().distance < wraith.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(wraith.attackStateMini);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, wraith.transform.position) > 7)
                stateMachine.ChangeState(wraith.idleState);
        }

        float desiredHeight = player.position.y + 1.5f;

        Vector3 newPosition = wraith.transform.position;
        newPosition.y = Mathf.Max(newPosition.y, desiredHeight); // Chọn giá trị cao nhất giữa độ cao hiện tại và độ cao mong muốn
        wraith.transform.position = newPosition;

        if (direction.x > 0)
            moveDir = 1;
        else if (direction.x < 0)
            moveDir = -1;

        wraith.SetVelocity(wraith.moveSpeed * direction.x, wraith.moveSpeed * direction.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
    private bool CanAttack()
    {
        if (Time.time >= wraith.lastTimeAttacked + wraith.attackCooldown)
        {
            wraith.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
