using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speedBullet = 0f;

    Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);

        // Tìm kiếm đối tượng Player trong scene và gán vào biến player
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Kiểm tra nếu player không tồn tại thì không thực hiện hành động bắn đạn
        if (player == null)
            return;

        // Lấy hướng vector từ vị trí của đạn đến vị trí của nhân vật
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Thiết lập vận tốc của đạn theo hướng mặt của nhân vật
        rb.velocity = direction * -speedBullet;
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Enemy"){
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(50); // Giảm 1 HP khi đạn va chạm với Enemy
            }
            Destroy(gameObject); // Hủy đạn sau khi va chạm với Enemy
        }


        if (collision.gameObject.tag == "TileMap")
        {
            Debug.Log("va cham");
            Destroy(gameObject); // Hủy đạn sau khi va chạm với Enemy
        }

    }
}
