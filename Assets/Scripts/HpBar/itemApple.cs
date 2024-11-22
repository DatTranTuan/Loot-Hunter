using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemApple : MonoBehaviour
{
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            CharacterStats apple = collision.gameObject.GetComponent<CharacterStats>();
            if (apple != null)
            {
                apple.healing(100); // Giảm 1 HP khi đạn va chạm với Enemy
            }
            Destroy(gameObject); // Hủy đạn sau khi va chạm với Enemy
        }

    }

}
