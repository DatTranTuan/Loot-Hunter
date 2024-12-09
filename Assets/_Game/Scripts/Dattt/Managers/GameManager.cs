using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject player;

    [SerializeField] private Transform currentCheckPoint;

    [SerializeField] private Transform spawnMap1;
    [SerializeField] private Transform spawnMap2;
    [SerializeField] private Transform spawnMap3;

    [SerializeField] private GameObject cthulu;
    [SerializeField] private GameObject nightBone;

    [SerializeField] private GameObject floatTextPrefab;

    [SerializeField] private List<BotControl_dattt> listBots = new List<BotControl_dattt>();

    public GameObject Cthulu { get => cthulu; set => cthulu = value; }
    public GameObject NightBone { get => nightBone; set => nightBone = value; }
    public Transform CurrentCheckPoint { get => currentCheckPoint; set => currentCheckPoint = value; }
    public GameObject FloatTextPrefab { get => floatTextPrefab; set => floatTextPrefab = value; }

    private void Start()
    {
        SoundManager.Instance.Play("BGM");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Immortal();
        }
    }

    public void Replay()
    {
        UIManager.Instance.DeathPanel.SetActive(false);

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
        else if (UIManager.Instance.Map3.gameObject.activeInHierarchy)
        {
            if (currentCheckPoint == null)
            {
                PlayerControl.Instance.transform.position = spawnMap3.position;
            }
            else
            {
                PlayerControl.Instance.transform.position = currentCheckPoint.position;
            }
        }

        if (UIManager.Instance.Map2.activeInHierarchy || UIManager.Instance.Map3.activeInHierarchy)
        {
            MageMate.Instance.ResetSpell();
        }

        PlayerControl.Instance.PlayerReset();

        ReloadAllBot();
    }

    public void ReloadAllBot()
    {
        for (int i = 0; i < listBots.Count; i++)
        {
            listBots[i].ReSpawn();
        }
    }

    public void StartDelayWinning()
    {
        StartCoroutine(DelayWinning());
    }

    public IEnumerator DelayWinning()
    {
        yield return new WaitForSeconds(2f);

        UIManager.Instance.WinPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SpawnFloatText()
    {
        GameObject spawnFloatText = Instantiate(floatTextPrefab, player.transform);
        spawnFloatText.GetComponentInChildren<TextMesh>().text = "+ 1";
        Destroy(spawnFloatText.gameObject, 1f);
    }

    public void Immortal()
    {
        PlayerControl.Instance.IsImune = !PlayerControl.Instance.IsImune;
    }
}
