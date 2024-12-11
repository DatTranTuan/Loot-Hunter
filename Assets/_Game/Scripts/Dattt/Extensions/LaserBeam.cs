using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    public void TurnOn()
    {
        boxCollider.enabled = true;
    }

    public void TurnOff() 
    {
        boxCollider.enabled = false;
        Destroy(gameObject);
    }

    private void Start()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (PlayerControl.Instance.transform.position - transform.position));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200f);
        transform.Rotate(0, 0, 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControl.Instance.PlayerTakeDmg(100);
        }
    }
}
