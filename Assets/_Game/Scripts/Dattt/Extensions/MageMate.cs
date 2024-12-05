using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMate : Singleton<MageMate>
{
    [SerializeField] private BotAnimation anim;
    [SerializeField] private PlayerControl player;

    [SerializeField] private FireBall fireBall;
    [SerializeField] private Transform fireBalllCon;

    [SerializeField] private GameObject reduceSpell;

    public BotAnimation Anim { get => anim; set => anim = value; }

    private void Update()
    {
        if (player.IsDeath)
        {
            Anim.Death();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Anim.Attack();
            SpawnFireBall();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(ActiveReduceSpell(5f));
        }
    }

    public void SpawnFireBall()
    {
        if (player.transform.right.x < 0)
        {
            FireBall fireSpawn = Instantiate(fireBall, fireBalllCon.position, Quaternion.Euler(0, -180, -180));
        }
        else
        {
            FireBall fireSpawn = Instantiate(fireBall, fireBalllCon.position, Quaternion.Euler(0, -180, 0));
        }
    }

    public void TurnOffAttack()
    {
        Anim.Idle();
    }

    public IEnumerator ActiveReduceSpell(float delay)
    {
        PlayerControl.Instance.IsReduce = true;
        reduceSpell.SetActive(true);

        yield return new WaitForSeconds(delay);

        PlayerControl.Instance.IsReduce = false;
        reduceSpell.SetActive(false);
    }
}
