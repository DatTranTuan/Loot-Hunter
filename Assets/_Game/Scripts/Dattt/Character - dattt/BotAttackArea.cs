using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackArea : MonoBehaviour
{
    private Dictionary<int, IHealthControlAble> dicTakeDmg = new Dictionary<int, IHealthControlAble>(); 

    private bool isAttack = false;
    private IHealthControlAble player;

    public IHealthControlAble PLAYER =>  player;

    private void Awake()
    {
        player = PlayerControl.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = PlayerControl.Instance;

        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Hit Player");

            int id = collision.gameObject.GetInstanceID();
            dicTakeDmg.TryAdd(id, player);

            //Debug.Log("Dmgggggggg");
            //Player_dattt.Instance.TakeDamage(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        }
        //this.gameObject.SetActive(false);

        //if (collision.gameObject.layer == 6 && !isAttack)
        //{
        //    Debug.Log("Dmgggggggg");
        //    Player_dattt.Instance.TakeDamage(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        //    isAttack = true;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            player = null;

            int id = collision.gameObject.GetInstanceID();
            dicTakeDmg.Remove(id);

            //Debug.Log("Dmgggggggg");
            //Player_dattt.Instance.TakeDamage(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        }
    }

    public void BotDealDmg(float dmg)
    {
        foreach (var item in dicTakeDmg.Values)
        {
            if (item == null) 
            { 
                break; 
            }
            else
            {
                item.PlayerTakeDmg(dmg);
            }
        }
    }

    private void OnDisable()
    {
        isAttack= false;
    }
}
