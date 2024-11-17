using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea_dattt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Charactor>().OnHitZb(5f);
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Charactor>().OnHit(5f);
        }
    }
}
