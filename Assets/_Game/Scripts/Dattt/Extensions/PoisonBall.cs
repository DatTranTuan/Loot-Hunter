using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControl.Instance.PlayerTakeDmg(100);
            Destroy(gameObject);
        }

        Destroy(gameObject, 5f);
    }
}
