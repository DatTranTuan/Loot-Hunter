using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    [SerializeField] private BotAnimation animator;

    private float randomDance;

    private float timer = 0f;
    [SerializeField] private float interval = 2f;

    void Start()
    {
        animator.Balance();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            RandomizeAnimation();
        }
    }

    void RandomizeAnimation()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < 25)
        {
            animator.Balance();
        }
        else if (randomValue < 50)
        {
            animator.Hip();
        }
        else if (randomValue < 75)
        {
            animator.Slide();
        }
        else
        {
            animator.Snap();
        }
    }
}
