using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea_Hieu : MonoBehaviour
{
    private Mushroom_Hieu mushroom_Hieu;

    void Start()
    {
        // Lấy tham chiếu đến script chính từ cùng một GameObject
        mushroom_Hieu = GetComponentInParent<Mushroom_Hieu>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            StartCoroutine(mushroom_Hieu.AttackPlayer());
        }
    }

    
}
