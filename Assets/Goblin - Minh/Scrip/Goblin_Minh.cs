using System.Collections;
using UnityEngine;

public class Goblin_Minh : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isAttacking = false;

    [SerializeField] private float speedMove = 3f;
    [SerializeField] private float attackRange = 2.0f;

    public SpriteRenderer sR;
    public Transform player;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    private float attackCooldown = 3f; // Thời gian chờ giữa các lần chém
    private float lastAttackTime = 0f; // Thời gian của lần chém trước

    [SerializeField] private int damageFromPlayer = 10; // Sát thương nhận từ player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else if (distanceToPlayer < 5f)
            {
                MoveTowardsPlayer();
            }
            else
            {
                SetIdle();
            }
        }
        else
        {
            SetIdle();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (new Vector2(player.position.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        Vector2 move = direction * speedMove * Time.deltaTime;
        transform.Translate(move);

        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                sR.transform.localScale = new Vector3(-Mathf.Abs(sR.transform.localScale.x), sR.transform.localScale.y, sR.transform.localScale.z);
            }
            else if (player.position.x > transform.position.x)
            {
                sR.transform.localScale = new Vector3(Mathf.Abs(sR.transform.localScale.x), sR.transform.localScale.y, sR.transform.localScale.z);
            }
        }

        anim.SetTrigger("Run");
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void SetIdle()
    {
        anim.SetTrigger("Idle");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Gọi hàm TakeDamage với sát thương khi va chạm với Player
            TakeDamage(damageFromPlayer);
            
        }
    }
}
