using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_dattt : MonoBehaviour , IHealthControlAble
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar_dattt healthBar;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth;

    private String currentAnimName;
    private float hp;

    public bool IsDead => hp <= 0;
    // if hp = 0 return IsDead = true else return IsDead = false;

    private void Awake()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        healthBar = GetComponentInChildren<HealthBar_dattt>();
        currentHealth = maxHealth;
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("Death");
    }


    public void TakeDamage(float damageTaken)
    {
        damageTaken = DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal;

        if (!Player_dattt.Instance.IsImune)
        {
            currentHealth -= damageTaken;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    //public override void TakeDmg(float dmg)
    //{

    //}

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void TakeDmg(float dmg)
    {
        dmg = DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal;

        if (!Player_dattt.Instance.IsImune)
        {
            currentHealth -= dmg;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
