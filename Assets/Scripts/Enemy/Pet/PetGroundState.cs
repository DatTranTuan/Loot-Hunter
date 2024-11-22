using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGroundState : EnemyState
{
    protected Pet pet;
    protected Player _player;
    protected Transform player;
    private int moveDir;

    public PetGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBollName, Pet pet) : base(_enemyBase, _stateMachine, _animBollName)
    {
        this.pet = pet;
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
        if (pet.IsPlayerDetected() || Vector2.Distance(pet.transform.position, player.position) < 10)
        {
            stateMachine.ChangeState(pet.moveState);
            pet.moveSpeed = 5;
            
        }
        if (pet.IsPlayerDetected().distance < pet.attackDistance)
        {
            stateMachine.ChangeState(pet.idleState);
            pet.moveSpeed = 0;
           
        }
        if (player.position.x < pet.transform.position.x)
        {
            pet.Flip();
        }

        if (player.position.x > pet.transform.position.x)
            moveDir = 1;
        else if(player.position.x < pet.transform.position.x)
            moveDir = -1;
        
        pet.SetVelocity(pet.moveSpeed * moveDir, rb.velocity.y);
    }
}
