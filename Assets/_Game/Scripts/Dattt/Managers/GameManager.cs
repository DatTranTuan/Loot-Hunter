using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform spawnMap1;
    [SerializeField] private Transform spawnMap2;

    [SerializeField] private List<BotControl_dattt> listBots = new List<BotControl_dattt>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Replay();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Immortal();
        }
    }

    public void Replay()
    {
        if (UIManager.Instance.Map1.gameObject.activeInHierarchy)
        {
            PlayerControl.Instance.transform.position = spawnMap1.position;
        }
        else if (UIManager.Instance.Map2.gameObject.activeInHierarchy)
        {
            PlayerControl.Instance.transform.position = spawnMap2.position;
        }

        MageMate.Instance.Anim.Idle();
        PlayerControl.Instance.CurrentHealth = PlayerControl.Instance.MaxHealth;
        PlayerControl.Instance.HealthBar.UpdateHealthBar(PlayerControl.Instance.CurrentHealth, PlayerControl.Instance.MaxHealth);
        PlayerControl.Instance.IsDeath = false;
        PlayerControl.Instance.PStateMachine.Exit(PlayerControl.Instance.PStateMachine.GetState(typeof(PS_Death)));
        PlayerControl.Instance.ChangeIdle();

        ReloadAllBot();
    }

    public void ReloadAllBot()
    {
        for (int i = 0; i < listBots.Count; i++)
        {
            listBots[i].ReSpawn();
        }
    }

    public void Immortal()
    {
        PlayerControl.Instance.IsImune = !PlayerControl.Instance.IsImune;
    }
}
