using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluDeath : MonoBehaviour
{
    [SerializeField] private GameObject princess;

    public void SaveThePrincess()
    {
        gameObject.SetActive(false);
        princess.SetActive(true);
    }
}
