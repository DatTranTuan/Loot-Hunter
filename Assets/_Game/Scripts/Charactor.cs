using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private string currentAnimName;

    private float hp;
    public bool isDead => hp <= 0;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 10;
    }

    public virtual void OnDesPam()
    {

    }
    protected virtual void OnDeath()
    {
        ChangeAnim("Death");
    }

    protected virtual void OnDeathZb()
    {
        ChangeAnim("Zb_Death");
        Invoke(nameof(OnDesPam), 3f);

    }
    public void OnHit(float damage)
    {
        Debug.Log("hitPlayer");
        if (!isDead)
        {
            hp -= damage;
        }
        if (isDead)
        {
            OnDeath();
            hp = 0;
          
        }
    }

    public void OnHitZb(float damage)
    {
        Debug.Log("hitZombie");
        if (!isDead)
        {
            hp -= damage;
        }
        if (isDead)
        {
            OnDeathZb();
            hp = 0;

        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }

    }
}
