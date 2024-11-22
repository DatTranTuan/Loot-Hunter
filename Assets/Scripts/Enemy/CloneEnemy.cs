using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemy : MonoBehaviour
{
    public GameObject cloneEnemy;
    void Start()
    {
        PWClone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PWClone()
    {
        Instantiate(cloneEnemy);
    }
}
