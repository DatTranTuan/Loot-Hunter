using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // components
    // dieu khien animation cua nhan vat
    private Chest chest;

    public Animator anim { get; private set; }
    // dieu hien vat ly
    public Rigidbody2D rb { get; private set; }
    // kieu khien hieu ung
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }

    public EntityFX fx { get; private set; }

    // Knockback info 
    // (Direction) huong knockBack khi bi tan cong
    [SerializeField] protected Vector2 knockbackDirection;

    // (Duration) thoi gian bi knockBack 
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    ///  collision info

    public Transform attackCkeck;


    // attackCkeck diem kiem tra tan cong
    public float attackCkeckRadius;
    [SerializeField] protected Transform groundCheck;
    // groundCheck pham vi duoi chan
    [SerializeField] protected float groundCheckDistance;
    // groundCheckDistance khoang cach kiem tra mat dat
    [SerializeField] protected Transform wallCheck;
    // wallCheck kiem tra tuong truoc mat
    [SerializeField] protected float wallCheckDistance;
    // wallCheckDistance khoang cach giua tuong
    [SerializeField] protected LayerMask whatIsGround;
    // loai mat dat


    // trai thai lat mat
    public int facingDir { get; private set; } = 1;
    // facingDir huong cua entity hien tai 
    protected bool facingRight = true;

    public System.Action OnFlipped;
    protected virtual void Awake()
    {

    }
    // hàm khởi tạo
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponentInChildren<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }
    protected virtual void Update()
    {

    }
    // tao ham thuc hien 1 cuoc tan cong
    public virtual void Damage()
    {
        fx.StartCoroutine("FLashFX");
    //    Debug.Log(gameObject.name + " was damaged");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    // collision
    // virtual de o che do cong khai
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        // thuc hien tan cong
        Gizmos.DrawWireSphere(attackCkeck.position, attackCkeckRadius); 
    }


    // xet vat ly
    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }



    // Flip trang thai lat mat
    public virtual void Flip()
    {
        // dao nguoc trang thai 
        facingDir = facingDir * -1;
        // cap nhat trang thai cua facingRight true=> fasle and false=> true
        facingRight = !facingRight;
        // doi h??ng
        transform.Rotate(0, 180, 0);
        if(OnFlipped != null)
        {
            OnFlipped();
        }
    }

    // xac dinh co can lat mat hay khong thong qua _x
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight) { Flip(); }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public virtual void Die()
    {

    }
    /*
    public void Attack()
    {
        // Kiểm tra xem attackCheck đã được gán chưa
        if (attackCheck == null)
        {
            Debug.LogWarning("Attack check transform is not assigned!");
            return;
        }

        // Kiểm tra xem có hòm rương trong phạm vi tấn công không
        Collider2D[] hitChests = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, whatIsChest);

        // Lặp qua tất cả các hòm rương va chạm
        foreach (Collider2D chestCollider in hitChests)
        {
            // Lấy thành phần Chest từ hòm rương
            chest = chestCollider.GetComponent<Chest>();

            // Kiểm tra xem chest có tồn tại và đang hoạt động không
            if (chest != null && chest.isActiveAndEnabled)
            {
                // Hủy bỏ hòm rương
                chest.DestroyChest();
            }
        }
    }


    [SerializeField] protected float attackRange;


        protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }
    */
}
