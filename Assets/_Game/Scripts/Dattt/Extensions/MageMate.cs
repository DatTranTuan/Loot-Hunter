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

    private bool isReady = false;

    private int reduceIndex = 5;
    private int fireBallIndex = 6;

    public BotAnimation Anim { get => anim; set => anim = value; }
    public bool IsReady { get => isReady; set => isReady = value; }
    public int ReduceIndex { get => reduceIndex; set => reduceIndex = value; }
    public int FireBallIndex { get => fireBallIndex; set => fireBallIndex = value; }

    private void Update()
    {
        if (player.IsDeath)
        {
            Anim.Death();
        }

        if (Input.GetKeyDown(KeyCode.V) && isReady && FireBallIndex > 0)
        {
            FireBallIndex--;
            Anim.Attack();
            SpawnFireBall();
            UIManager.Instance.FireBallText.text = fireBallIndex.ToString();
        }

        if (Input.GetKeyDown(KeyCode.S) && isReady && ReduceIndex > 0)
        {
            StartCoroutine(ActiveReduceSpell(5f));
            ReduceIndex--;
            UIManager.Instance.ReduceText.text = reduceIndex.ToString();
        }
    }

    public void ResetSpell()
    {
        anim.Idle();

        reduceIndex = 5;
        fireBallIndex = 6;

        UIManager.Instance.FireBallText.text = fireBallIndex.ToString();
        UIManager.Instance.ReduceText.text = reduceIndex.ToString();
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
        isReady = false;
        PlayerControl.Instance.IsReduce = true;
        reduceSpell.SetActive(true);

        yield return new WaitForSeconds(delay);

        isReady = true;
        PlayerControl.Instance.IsReduce = false;
        reduceSpell.SetActive(false);
    }
}
