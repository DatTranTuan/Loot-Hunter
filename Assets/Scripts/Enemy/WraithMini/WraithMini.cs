using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class WraithMini : Enemy
{
    public float left, right;
    public float speed = 1;


    // khai bao 
    public WraithMiniIdleState wraithMiniIdle {  get; private set; }
    public WraithMiniMoveState wraithMiniMove { get; private set; }
    public WraithMiniAttackState wraithMiniAttack { get; private set; }
    public WraithMiniBattleState battleState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        wraithMiniIdle = new WraithMiniIdleState(this, stateMachine, "Idle",this);
        wraithMiniMove = new WraithMiniMoveState(this, stateMachine, "Move", this);
        wraithMiniAttack = new WraithMiniAttackState(this, stateMachine, "Attack", this);
        battleState = new WraithMiniBattleState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(wraithMiniIdle);
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
