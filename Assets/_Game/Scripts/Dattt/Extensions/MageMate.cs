using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMate : MonoBehaviour
{
    [SerializeField] private BotAnimation anim;
    [SerializeField] private PlayerControl player;

    private void Update()
    {
        if (player.IsDeath)
        {
            anim.Death();
        }
    }
}
