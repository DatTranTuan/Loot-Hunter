using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PurpleWitch : Enemy
{
    // Start is called before the first frame update
    public float left, right;
    public float speed = 1;
    public GameObject ClonePW;
    public Transform PlayerPos;

    // Remove the redeclaration of stateMachine here

    public PurpleWitchMoveState moveState { get; private set; }
    public PurpleWitchIdleState idleState { get; private set; }
    public PurpleWitchAttackState attackState { get; private set; }
    public PurpleWitchBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        moveState = new PurpleWitchMoveState(this, stateMachine, "Move", this);
        idleState = new PurpleWitchIdleState(this, stateMachine, "Idle", this);
        attackState = new PurpleWitchAttackState(this, stateMachine, "Attack", this);
        battleState = new PurpleWitchBattleState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
