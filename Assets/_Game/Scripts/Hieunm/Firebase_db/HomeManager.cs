using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeManager : Singleton<HomeManager>
{
    public FirebaseManager firebaseManager;
    private FirebaseAuth auth;
    private DatabaseReference databaseReference;
    public GameObject loginPanel;

    [Header("Home Panel")]
    public TMP_Text userNameText; // Hiển thị tên người chơi
    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;
    public Button quitAccountButton;
    public Button HighScoreButton;
    public GameObject homePanel;
    public GameObject highScorePanel;

    public GameObject datPanel;

    [Header("Settings Panel")]
    public GameObject settingsPanel;


    [SerializeField] private GameObject allMap;

    [SerializeField] private GameObject hieunm;
    [SerializeField] private GameObject eventSystem;

    public GameObject EventSystem { get => eventSystem; set => eventSystem = value; }

    void Start()
    {
        // Khởi tạo Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Gán sự kiện cho các nút
        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(ShowSettings);
        quitButton.onClick.AddListener(QuitGame);
        quitAccountButton.onClick.AddListener(QuitAccount);
        HighScoreButton.onClick.AddListener(HighScoreTop);

    }

    private void NewGame()
    {
        eventSystem.SetActive(false);

        Debug.Log("Starting a new game...");
        loginPanel.SetActive(false);
        homePanel.SetActive(false);
        allMap.SetActive(true);
        DataLevelManager.Instance.NewGame();
    }

    private void ContinueGame()
    {
        eventSystem.SetActive(false);

        Debug.Log("Continuing the game...");
        // Thêm logic tiếp tục game (tải dữ liệu cũ)

        loginPanel.SetActive(false);
        homePanel.SetActive(false);

        allMap.SetActive(true);

        DataLevelManager.Instance.ContinueGame();
        GameManager.Instance.CheckSpawnPos();
    }

    private void OpenSettings()
    {
        Debug.Log("Opening settings...");
        
    }

    private void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    private void QuitAccount()
    {
        Debug.Log("Logging out...");
        auth.SignOut(); // Đăng xuất tài khoản

        // Làm sạch dữ liệu tạm thời
        ClearUserNameDisplay();
        ShowLogin();
    }

    public void ShowHome()
    {
        settingsPanel.SetActive(false);
        highScorePanel.SetActive(false);
    }    

    private void ShowLogin()
    {
        loginPanel.SetActive(true); // Chuyển về màn hình đăng nhập
        homePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }    

    private void ShowSettings()
    {
        settingsPanel.SetActive(true);
        homePanel.SetActive(true);
    }

    private void ClearUserNameDisplay()
    {
        userNameText.text = "Welcome!";
        Debug.Log("Cleared username display.");
    }

    private void HighScoreTop()
    {
        highScorePanel.SetActive(true);
        settingsPanel.SetActive(false);
        loginPanel.SetActive(false);
    }
}
