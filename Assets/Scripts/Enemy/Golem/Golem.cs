using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{

    public float left, right;
    public float speed = 1;


    public GolemMoveState moveState { get; private set; }
    public GolemIdleState idleState { get; private set; }
    public GolemAttackState attackState { get; private set; }

    public GolemBattleState battleState { get; private set; }

    public GolemDeadState deadState { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        moveState = new GolemMoveState(this, stateMachine, "Move", this);
        idleState = new GolemIdleState(this, stateMachine, "Idle",this);
        battleState = new GolemBattleState(this, stateMachine, "Move", this);
        attackState = new GolemAttackState(this, stateMachine, "Attack",this);
        deadState = new GolemDeadState(this, stateMachine, "Die",this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        Destroy(gameObject);
    }
}
