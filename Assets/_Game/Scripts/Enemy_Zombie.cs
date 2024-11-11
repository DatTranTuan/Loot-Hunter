using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Zombie : Charactor
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    
    private IState currentState;
    private bool isRight = true;
    [SerializeField] private Charactor target;

    public Charactor Target => target;
    private void Update()
    {
       
        if(currentState != null)
        {
            currentState.OnExcute(this);

        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        attackArea.SetActive(false);
    }
    public override void OnDesPam()
    {
        base.OnDesPam();
        Destroy(gameObject);
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
       

        if (currentState != null) 
        { 
            currentState.OnEnter(this);    
        }
    }
    public void Moving()
    {
            ChangeAnim("Zb_Run");
            rb.velocity = transform.right * moveSpeed;

    }

    public void StopMoving()
    {
        ChangeAnim("Zb_Idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnim("Zb_attack");
        attackArea.SetActive(true);
    }

    internal void setTarget(Charactor charactor)
    {
        this.target = charactor;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWall"))
        {
                ChangeDirection(!isRight);
        }
    }


    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
            
    }

    
}