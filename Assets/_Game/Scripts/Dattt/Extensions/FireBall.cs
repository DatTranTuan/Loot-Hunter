using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBall : MonoBehaviour
{
    private BotControl_dattt botControl;

    [SerializeField] private float speed;

    Vector3 moveDirection;

    private void Start()
    {
        moveDirection = PlayerControl.Instance.transform.right;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            botControl = collision.GetComponent<BotControl_dattt>();
            botControl.ChangeTakeHit();
            Destroy(gameObject);
        }

        Destroy(gameObject, 5f);
    }
}
