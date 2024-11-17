using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BotControl_dattt : Singleton<BotControl_dattt>
{
    [SerializeField] private BotAnimation anim;

    private StateMachine stateMachine;

    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private BotType botType;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    [SerializeField] private BotAttackArea attackArea;

    private IState currentState;
    private float currentHealth;

    private float damageTaken;

    private bool isRight = true;
    private bool isTarget = false;
    private bool isTakeDmg = false;

    public BotAnimation Anim { get => anim; set => anim = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    public bool IsTarget { get => isTarget; set => isTarget = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsTakeDmg { get => isTakeDmg; set => isTakeDmg = value; }
    public float DamageTaken { get => damageTaken; set => damageTaken = value; }
    public BotType BotType { get => botType; set => botType = value; }

    private void Start()
    {
        StateInit();

        currentHealth = DataManager.Instance.GetBotData(BotType).maxHealth;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        stateMachine.Update();

        IsTarget = CheckPlayer();
    }

    private void StateInit()
    {
        StateMachine = new StateMachine(this);
        S_Wait s_Idle = new S_Wait(this);
        StateMachine.AddState(s_Idle);
        s_Idle.Enter();
    }

    public void ChangePatrol()
    {
        if (stateMachine.GetState(typeof(S_Patrol)) == null)
        {
            stateMachine.AddState(new S_Patrol(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Patrol)));
    }

    public void ChangeIdle()
    {
        if (stateMachine.GetState(typeof(S_Wait)) == null)
        {
            stateMachine.AddState(new S_Wait(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Wait)));
    }

    public void ChangeAttack()
    {
        if (stateMachine.GetState(typeof(S_Attack)) == null)
        {
            stateMachine.AddState(new S_Attack(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Attack)));
    }

    public void ChangeTakeHit()
    {
        if (stateMachine.ActiveStates.Count > 0)
        {
            IStateNormal currentState = stateMachine.ActiveStates[stateMachine.ActiveStates.Count - 1];
            stateMachine.RemoveState(currentState);
        }

        if (stateMachine.GetState(typeof(S_TakeHit)) == null)
        {
            stateMachine.AddState(new S_TakeHit(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_TakeHit)));
    }

    public void ChangeDeath()
    {
        if (stateMachine.GetState(typeof(S_Death)) == null)
        {
            stateMachine.AddState(new S_Death(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Death)));
    }

    public void Moving()
    {
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        //edgeCollider.enabled = true;

        ActiveAttack();
        Debug.Log("UnLeash Sword");
        //StartCoroutine(DelayActiveBox());
        //Invoke(nameof(DeActiveAttack), 0.5f);
    }
    private IEnumerator DelayActiveBox() 
    {
        yield return new WaitForSeconds(0.15f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack),0.15f);
    }

    public bool IsTargetInRange()
    {
        if (IsTarget && Vector2.Distance(Player_dattt.Instance.transform.position, transform.position) <= attackRange)
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
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private int indexAttack = 0;
    private void ActiveAttack()
    {
        //attackArea.PLAYER?.TakeDmg(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        attackArea.DealDmg(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        Debug.Log("Dmgggggggg");


        //edgeCollider.enabled = true;
        //attackArea.SetActive(true);
        //indexAttack++;
        //Debug.LogError($"Time On: {Time.frameCount}, index {indexAttack}");
    }

    private void DeActiveAttack()
    {
        //edgeCollider.enabled = false;

        //attackArea.SetActive(false);
        //Debug.LogError($"Time OFF: {Time.frameCount},index {indexAttack}");
    }

    public bool CheckPlayer()
    {
        Vector2 raycastStartPos = new Vector2(transform.position.x, transform.position.y + 1f);

        RaycastHit2D hitRight = Physics2D.Raycast(raycastStartPos, Vector2.right, 2.5f, playerLayer);
        Debug.DrawRay(raycastStartPos, Vector2.right * 3f, Color.green);

        RaycastHit2D hitLeft = Physics2D.Raycast(raycastStartPos, Vector2.left, 2.5f, playerLayer);
        Debug.DrawRay(raycastStartPos, Vector2.left * 3f, Color.red);

        return hitRight.collider != null || hitLeft.collider != null;
    }
}
