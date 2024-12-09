using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform currentCheckPoint;

    [SerializeField] private Transform spawnMap1;
    [SerializeField] private Transform spawnMap2;

    [SerializeField] private GameObject cthulu;
    [SerializeField] private GameObject nightBone;

    [SerializeField] private List<BotControl_dattt> listBots = new List<BotControl_dattt>();

    public GameObject Cthulu { get => cthulu; set => cthulu = value; }
    public GameObject NightBone { get => nightBone; set => nightBone = value; }
    public Transform CurrentTransform { get => currentCheckPoint; set => currentCheckPoint = value; }

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
            if (currentCheckPoint == null)
            {
                PlayerControl.Instance.transform.position = spawnMap1.position;
            }
            else
            {
                PlayerControl.Instance.transform.position = currentCheckPoint.position;
            }
        }
        else if (UIManager.Instance.Map2.gameObject.activeInHierarchy)
        {
            if (currentCheckPoint == null)
            {
                PlayerControl.Instance.transform.position = spawnMap2.position;
            }
            else
            {
                PlayerControl.Instance.transform.position = currentCheckPoint.position;
            }
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
