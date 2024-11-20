using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab; // Prefab của item quả táo

    // Hàm này được gọi từ hiệu ứng vụ nổ để bắt đầu tạo ra item quả táo
    public void SpawnApple()
    {
        Instantiate(applePrefab, transform.position, Quaternion.identity);
    }
}
