using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_dattt : Character_dattt
{
    private static Player_dattt instance;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private float rollForce = 350;

    [SerializeField] private GameObject attackArea;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;
    [SerializeField] private bool isImune = false;
    private bool isRolling = false;

    private float horizontal;

    private Vector2 targetPosition;

    public static Player_dattt Instance { get => instance; set => instance = value; }
    public bool IsImune { get => isImune; set => isImune = value; }

    private void Awake()
    {
        instance = this; 
    }

    void Update()
    {
        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");

        if (currentHealth <= 0)
        {
            OnDeath();
        }

        if (!isDeath)
        {
            if (isAttack)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            if (isGrounded && !isJumping && !isRolling && !isAttack)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Jump();
                    isJumping = false;
                }

                if (Mathf.Abs(horizontal) > 0.1f)
                {
                    ChangeAnim("Run");
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    Attack();
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    Roll();
                }
            }

            if (!isGrounded && rb.velocity.y < 0)
            {
                ChangeAnim("JumpFall");
                isJumping = false;
            }

            if (Mathf.Abs(horizontal) > 0.1f && !isRolling)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));

            }
            else if (isGrounded && !isAttack && !isImune && !isRolling)
            {
                ChangeAnim("Idle");
                rb.velocity = Vector2.zero;
            }
        }
    }

    protected override void OnDeath()
    {
        isDeath = true;
        ChangeAnim("DeathNoMove");
    }

    public void Attack()
    {
        ChangeAnim("Attack");
        isAttack = true;
        isRolling = true;
        ActiveAttack();
        Invoke(nameof(ResetAttack), 0.4f);
        Invoke(nameof(DeActiveAttack), 0.4f);
    }

    public void Roll()
    {
        //targetPosition = new Vector2(transform.position.x + transform.right.x * rollForce, transform.position.y);
        //Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(transform.right.x * rollForce, 0));
        //Debug.Log(targetPosition);

        ChangeAnim("Roll");
        IsImune = true;
        isRolling = true;
    }

    public void DeActiveRoll()
    {
        Debug.Log("DeRolll");
        ChangeAnim("Idle");
        IsImune = false;
        isRolling = false;
    }

    public void Jump()
    {
        isJumping = true;
        ChangeAnim("Jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("Idle");
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
        isRolling = false;
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.025f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.025f, groundLayer);
        return hit.collider != null;
    }
}
