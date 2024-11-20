using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab của hiệu ứng vụ nổ
    public void DestroyChest()
    {
        // Gọi phương thức SpawnExplosion() sau một khoảng thời gian trễ
        Invoke("SpawnExplosion", 0.2f);

        // Hủy bỏ hòm rương sau một khoảng thời gian
        Destroy(gameObject, 0.2f);
    }

    private void SpawnExplosion()
    {
        // Tạo ra hiệu ứng vụ nổ
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Hủy bỏ hiệu ứng vụ nổ sau một khoảng thời gian
        Destroy(explosion, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyChest();
        }

    }
}
