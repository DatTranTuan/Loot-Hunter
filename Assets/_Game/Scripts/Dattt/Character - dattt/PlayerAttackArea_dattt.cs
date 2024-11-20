using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea_dattt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BotControl_dattt.Instance.ChangeTakeHit();
        }
    }
}
