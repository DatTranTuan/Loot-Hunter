using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleDoor : MonoBehaviour
{
    [SerializeField] private Transform nextPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControl.Instance.transform.position = nextPos.position;
        }
    }
}
