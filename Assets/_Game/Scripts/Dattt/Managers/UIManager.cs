using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject eventSystem;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endGamePanel;

    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button restart2Btn;
    [SerializeField] private Button quitMenuBtn;

    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button backEndBtn;

    [SerializeField] private GameObject map1;
    [SerializeField] private GameObject map2;
    [SerializeField] private GameObject map3;
    [SerializeField] private GameObject allMap;
    [SerializeField] private GameObject homePanel;

    [SerializeField] private GameObject mageMate;
    [SerializeField] private Text reduceText;
    [SerializeField] private Text fireBallText;

    public GameObject WinPanel { get => winPanel; set => winPanel = value; }
    public GameObject Map1 { get => map1; set => map1 = value; }
    public GameObject Map2 { get => map2; set => map2 = value; }
    public GameObject Map3 { get => map3; set => map3 = value; }
    public Text FireBallText { get => fireBallText; set => fireBallText = value; }
    public Text ReduceText { get => reduceText; set => reduceText = value; }
    public GameObject PlayPanel { get => playPanel; set => playPanel = value; }
    public GameObject DeathPanel { get => DeathPanel1; set => DeathPanel1 = value; }
    public GameObject DeathPanel1 { get => deathPanel; set => deathPanel = value; }
    public GameObject EndGamePanel { get => endGamePanel; set => endGamePanel = value; }

    private void Start()
    {
        if (map2.activeInHierarchy || map3.activeInHierarchy)
        {
            mageMate.SetActive(true);
            MageMate.Instance.IsReady = true;

            playPanel.SetActive(true);
            reduceText.text = MageMate.Instance.ReduceIndex.ToString();
            fireBallText.text = MageMate.Instance.FireBallIndex.ToString();
        }

        nextLevelBtn.onClick.AddListener(ClickNextLevel);
        restartBtn.onClick.AddListener(ClickRestartBtn);
        replayBtn.onClick.AddListener(GameManager.Instance.Replay);
        backEndBtn.onClick.AddListener(ClickBackBtn);

        pauseBtn.onClick.AddListener(ClickPauseBtn);
        resumeBtn.onClick.AddListener(ClickResumeBtn);
        restart2Btn.onClick.AddListener(ClickRestartBtn);
        quitMenuBtn.onClick.AddListener(ClickQuitMenuBtn);
    }

    public void ClickNextLevel()
    {
        if (map1.activeInHierarchy)
        {
            winPanel.SetActive(false);

            //Map1.gameObject.SetActive(false);
            //Map2.gameObject.SetActive(true);

            mageMate.SetActive(true);
            Time.timeScale = 1f;
        }
        else if (map2.activeInHierarchy)
        {
            winPanel.SetActive(false);

            //Map2.gameObject.SetActive(false);
            //Map3.gameObject.SetActive(true);

            Time.timeScale = 1f;
        }

        DataLevelManager.Instance.NextLevel();
        GameManager.Instance.CheckSpawnPos();

        playPanel.SetActive(true);

        MageMate.Instance.IsReady = true;
        MageMate.Instance.ResetSpell();

        PlayerControl.Instance.PlayerReset();

        GameManager.Instance.CurrentCheckPoint = null;

    }

    public void ClickRestartBtn ()
    {
        GameManager.Instance.CurrentCheckPoint = null;
        GameManager.Instance.Replay();
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void ClickBackBtn()
    {
        endGamePanel.SetActive(false);
        allMap.SetActive(false);
        homePanel.SetActive(true);

        HomeManager.Instance.EventSystem.SetActive(true);
        this.eventSystem.SetActive(false);
    }

    public void ClickPauseBtn()
    {
        pausePanel.SetActive(true);
    }

    public void ClickResumeBtn()
    {
        pausePanel.SetActive(false);
    }

    public void ClickQuitMenuBtn()
    {
        allMap.SetActive(false);
        homePanel.SetActive(true);
    }
}
