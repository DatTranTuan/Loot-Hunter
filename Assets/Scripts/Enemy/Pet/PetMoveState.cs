using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMoveState : PetGroundState
{
    public PetMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Pet pet) : base(_enemyBase, _stateMachine, _animBollName, pet)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        

    }
}
