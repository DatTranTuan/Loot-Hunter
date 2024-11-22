using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wraith_immirtal : MonoBehaviour
{
   // public GameObject wraithMiniPrefab; // Prefab của Wraith Mini
   // public Transform spawnPoint; // Điểm sinh ra Wraith Mini
    public float immuneDuration = 5f; // Thời gian bất tử (giây)
    private bool isImmune = false; // Trạng thái bất tử
    private float immuneTimer = 0f; // Bộ đếm thời gian bất tử
    private Collider2D npcCollider; // Collider của NPC
    private Animator animator; // Animator của NPC

    private void Awake()
    {
        npcCollider = GetComponent<Collider2D>(); // Lấy collider của NPC
        animator = GetComponentInChildren<Animator>(); // Lấy Animator của NPC
    }

   

    private void Update()
    {
        if (isImmune)
        {
            // Giảm thời gian bất tử
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0)
            {
                // Hết thời gian bất tử, kết thúc trạng thái bất tử
                MakeImmune(false);
            }
        }
    }

    // Hàm kích hoạt hoặc hủy trạng thái bất tử
    public void MakeImmune(bool immune)
    {
        isImmune = immune;
        if (isImmune)
        {
            // Kích hoạt trạng thái bất tử trong Animator
            animator.SetBool("IsImmune", true);
            animator.SetBool("Move", false);

            // Tắt collider
            npcCollider.enabled = false;
            // Bắt đầu đếm thời gian bất tử
            immuneTimer = immuneDuration;
            // Kêu gọi tiếp viện (sinh ra Wraith Mini)
           // SpawnWraithMini();
        }
        else
        {
            // Tắt trạng thái bất tử trong Animator
            animator.SetBool("IsImmune", false);
            animator.SetBool("Move", true);


            // Bật lại collider
            npcCollider.enabled = true;
        }
    }

    // Sinh ra Wraith Mini
    /*
    private void SpawnWraithMini()
    {
        GameObject wraithMini = Instantiate(wraithMiniPrefab, spawnPoint.position, Quaternion.identity);
        // Cài đặt các thuộc tính cho Wraith Mini nếu cần
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet")) // Kiểm tra va chạm với đạn của người chơi
        {
            // Kích hoạt trạng thái bất tử khi bị tấn công
            MakeImmune(true);
            Debug.Log("bất tử công");
        }
    }
}
