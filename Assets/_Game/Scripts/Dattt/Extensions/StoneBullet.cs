using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBullet : MonoBehaviour
{
    private Transform playerPos;

    private void Start()
    {
        playerPos = PlayerControl.Instance.transform;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 5f * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (playerPos.position - transform.position));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200f);
        transform.Rotate(0, 0, 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Golem Boss bullet dmg
            PlayerControl.Instance.PlayerTakeDmg(40);
            Destroy(gameObject);
        }
    }
}
