using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singleton<PlayerControl>, IHealthControlAble
{
    [SerializeField] private PlayerAnimation anim;

    private PlayerStateMachine pStateMachine;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private float rollForce = 350;
    [SerializeField] private float currentHealth;
    [SerializeField] private HealthBar_dattt healthBar;
    [SerializeField] private float maxHealth;

    [SerializeField] private bool isImune = false;

    [SerializeField] private GameObject attackArea;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;
    private bool isRolling = false;

    private float horizontal;

    private Vector2 targetPosition;

    public bool IsImune { get => isImune; set => isImune = value; }
    public PlayerAnimation Anim { get => anim; set => anim = value; }
    public float Horizontal { get => horizontal; set => horizontal = value; }
    public bool IsRolling { get => isRolling; set => isRolling = value; }
    public PlayerStateMachine PStateMachine { get => pStateMachine; set => pStateMachine = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public LayerMask GroundLayer { get => groundLayer; set => groundLayer = value; }
    public float Speed { get => speed; set => speed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float RollForce { get => rollForce; set => rollForce = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public HealthBar_dattt HealthBar { get => healthBar; set => healthBar = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Start()
    {
        StateInit();
    }

    private void Update()
    {
        PStateMachine.Update();

        Debug.LogError(pStateMachine.ActivePStates[0]);

        IsGrounded = CheckGrounded();
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (IsDeath)
        {
            return;
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            isJumping = false;
        }

        if (IsAttack)
        {
            Rb.velocity = Vector2.zero;
            return;
        }
    }

    private void StateInit()
    {
        PStateMachine = new PlayerStateMachine(this);
        PS_Idle pS_Idle = new PS_Idle(this);
        PStateMachine.AddState(pS_Idle);
        pS_Idle.Enter();
    }

    public void ChangeIdle()
    {
        if (PStateMachine.GetState(typeof(PS_Idle)) == null)
        {
            PStateMachine.AddState(new PS_Idle(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Idle)));
    }

    public void ChangeMoving()
    {
        if (PStateMachine.GetState(typeof(PS_Moving)) == null)
        {
            PStateMachine.AddState(new PS_Moving(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Moving)));
    }

    public void ChangeJumping()
    {
        if (PStateMachine.GetState(typeof(PS_Jump)) == null)
        {
            PStateMachine.AddState(new PS_Jump(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Jump)));
    }

    public void ChangeAttack()
    {
        if (PStateMachine.GetState(typeof(PS_Attack)) == null)
        {
            PStateMachine.AddState(new PS_Attack(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Attack)));
    }

    public void ChangeRoll()
    {
        if (PStateMachine.GetState(typeof(PS_Roll)) == null)
        {
            PStateMachine.AddState(new PS_Roll(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Roll)));
    }

    public void ChangeTakeHit()
    {
        if (!isDeath)
        {
            if (pStateMachine.ActivePStates.Count > 0)
            {
                IStatePlayer currentState = pStateMachine.ActivePStates[pStateMachine.ActivePStates.Count - 1];
                pStateMachine.RemoveState(currentState);
            }

            if (pStateMachine.GetState(typeof(PS_TakeHit)) == null)
            {
                pStateMachine.AddState(new PS_TakeHit(this));
            }

            pStateMachine.ChangeState(pStateMachine.GetState(typeof(PS_TakeHit)));
        }
    }

    public void ChangeDeath()
    {
        if (PStateMachine.GetState(typeof(PS_Death)) == null)
        {
            PStateMachine.AddState(new PS_Death(this));
        }

        PStateMachine.ChangeState(PStateMachine.GetState(typeof(PS_Death)));
    }

    public void Death()
    {
        IsDeath = true;
    }

    public void Attack()
    {
        IsAttack = true;
        IsRolling = true;
        ActiveAttack();
        Invoke(nameof(ResetAttack), 0.4f);
        Invoke(nameof(DeActiveAttack), 0.4f);
    }

    private void ResetAttack()
    {
        IsAttack = false;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
        IsRolling = false;
    }

    public void DeActiveRoll()
    {
        Debug.Log("DeRolll");
        IsImune = false;
        IsRolling = false;
    }

    //public void TakeDamage(float damageTaken)
    //{
    //    damageTaken = DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal;

    //    if (!IsImune)
    //    {
    //        currentHealth -= damageTaken;
    //        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    //    }
    //}

    public void PlayerTakeDmg(float dmg)
    {
        dmg = DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal;

        if (!IsImune)
        {
            CurrentHealth -= dmg;
            HealthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
            ChangeTakeHit();
        }
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.025f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.025f, GroundLayer);
        return hit.collider != null;
    }
}
