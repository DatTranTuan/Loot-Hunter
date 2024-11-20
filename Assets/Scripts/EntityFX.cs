using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    // Flash FX 
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    private IEnumerator FLashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    }
}
