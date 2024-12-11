    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_namdd : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInTrigger = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Nếu player vẫn ở trong trigger, đảm bảo animation Attack đang hoạt động
        if (isPlayerInTrigger)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            // Nếu player không còn trong trigger, đảm bảo animation Attack không hoạt động
            animator.SetBool("Attack", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Đánh dấu player đã vào trigger
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Khi player rời khỏi trigger, đặt isPlayerInTrigger thành false
            isPlayerInTrigger = false;
            // Bắt đầu coroutine để chờ animation kết thúc
            StartCoroutine(WaitForAttackAnimation());
        }
    }

    private IEnumerator WaitForAttackAnimation()
    {
        // Chờ cho đến khi animation tấn công hoàn tất
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Gọi hàm để quay lại trạng thái idle
        OnAttackAnimationEnd();
    }

    // Hàm này sẽ được gọi khi animation tấn công kết thúc
    public void OnAttackAnimationEnd()
    {
        // Quay lại trạng thái idle
        animator.SetBool("Attack", false);
    }
}
