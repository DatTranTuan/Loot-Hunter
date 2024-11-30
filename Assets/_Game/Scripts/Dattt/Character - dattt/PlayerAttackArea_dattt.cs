using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea_dattt : MonoBehaviour
{
    private BotControl_dattt botControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            botControl = collision.GetComponent<BotControl_dattt>();
            botControl.ChangeTakeHit();
        }
    }
}
