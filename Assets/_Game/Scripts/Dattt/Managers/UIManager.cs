using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button nextLevelBtn;

    [SerializeField] private GameObject map1;
    [SerializeField] private GameObject map2;

    public GameObject WinPanel { get => winPanel; set => winPanel = value; }
    public GameObject Map1 { get => map1; set => map1 = value; }
    public GameObject Map2 { get => map2; set => map2 = value; }

    private void Start()
    {
        nextLevelBtn.onClick.AddListener(ClickNextLevel);
    }

    public void ClickNextLevel()
    {
        Map1.gameObject.SetActive(false);
        nextLevelBtn.gameObject.SetActive(false);
        Map2.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
}
