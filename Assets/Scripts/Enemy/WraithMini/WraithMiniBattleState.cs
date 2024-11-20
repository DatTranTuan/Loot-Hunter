using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMiniBattleState : EnemyState
{
    private Transform player;
    private WraithMini wraithMini;
    private int moveDir;
    public WraithMiniBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, WraithMini _wraithMini) : base(_enemyBase, _stateMachine, _animBollName)
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

        // Tính toán hướng đi hướng về người chơi
        Vector2 direction = (player.position - wraithMini.transform.position).normalized;

        if (wraithMini.IsPlayerDetected())
        {
            stateTimer = wraithMini.BattleTime;

            if (wraithMini.IsPlayerDetected().distance < wraithMini.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(wraithMini.wraithMiniAttack);
            }
        }

        // Độ cao mong muốn của đối thủ so với độ cao của người chơi
        float desiredHeight = player.position.y + 1.5f; // Độ cao mong muốn là độ cao của người chơi cộng thêm 2f

        // Điều chỉnh độ cao của đối thủ
        Vector3 newPosition = wraithMini.transform.position;
        newPosition.y = Mathf.Max(newPosition.y, desiredHeight); // Chọn giá trị cao nhất giữa độ cao hiện tại và độ cao mong muốn
        wraithMini.transform.position = newPosition;

        if (direction.x > 0) 
            moveDir = 1;
        else if (direction.x < 0)
            moveDir = -1;

        wraithMini.SetVelocity(wraithMini.moveSpeed * direction.x, wraithMini.moveSpeed * direction.y);
    }

    private bool CanAttack()
    {
        if (Time.time >=wraithMini.lastTimeAttacked + wraithMini.attackCooldown)
        {
            wraithMini.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
