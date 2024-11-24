using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mushroom_Hieu : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPlayerInArea = false;
    private bool isAttacking = false;
    [SerializeField] private float speedMove = 3f;

    public SpriteRenderer sR;
    public Transform player;

    //EnemyHealth
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private Healthbar_Hieu healthbar;

    // Attack settings
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackCooldown = 1f; // Time between attacks
    private float lastAttackTime = 0f;
    private bool canAttack = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthbar = GetComponentInChildren<Healthbar_Hieu>();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

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
            else if (distanceToPlayer < 3f)
            {
                MoveTowardsPlayer();
            }
            else
            {
                anim.SetTrigger("Mushroom_idle");
            }
        }
        else
        {
            anim.SetTrigger("Mushroom_idle");
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

        anim.SetTrigger("Mushroom_run");
    }

    public IEnumerator AttackPlayer()
    {
        // Kiểm tra khoảng cách trước khi tấn công
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            yield break; // Dừng nếu người chơi đã ra khỏi vùng tấn công
        }

        canAttack = false;
        anim.SetTrigger("Mushroom_attack");

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true; // Sẵn sàng cho lần tấn công kế tiếp

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }

        if (collision.CompareTag("AttackZone_Hieu") && canAttack)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInArea = false;
            anim.SetTrigger("Mushroom_idle");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        healthbar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Mushroom_hit");
        }
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Mushroom_death");
        Destroy(gameObject, 1.5f);
    }


}



