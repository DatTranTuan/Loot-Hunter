using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetIdleState : PetGroundState
{
    public PetIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Pet pet) : base(_enemyBase, _stateMachine, _animBollName, pet)
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
        if (!pet.IsPlayerDetected())
        {
            stateMachine.ChangeState(pet.idleState);
        }
    }
}
