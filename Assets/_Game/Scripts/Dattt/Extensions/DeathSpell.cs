using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpell : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    private Transform playerTrans;

    private void Start()
    {
        playerTrans = PlayerControl.Instance.transform;

        Vector3 newPosition = playerTrans.position + new Vector3(0, 2.7f, 0);

        transform.position = newPosition;
    }

    public void ActiveSpell()
    {
        boxCollider.enabled = true;
    }

    public void DeActiveSpell()
    {
        boxCollider.enabled = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControl.Instance.PlayerTakeDmg(90);
        }
    }
}
