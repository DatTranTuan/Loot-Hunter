using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : Enemy
{

    public float speed = 1;

    public PetMoveState moveState {  get; private set; }
    public PetIdleState idleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        moveState = new PetMoveState(this, stateMachine, "Move", this);
        idleState = new PetIdleState(this, stateMachine, "Idle", this);
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
}
