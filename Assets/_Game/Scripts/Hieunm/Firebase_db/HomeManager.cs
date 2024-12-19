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
    public GameObject AllMap { get => allMap; set => allMap = value; }

    void Start()
    {
        CheckFirebaseDependencies();
    }

    private void CheckFirebaseDependencies()
    {
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        //{
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        InitializeFirebase();
        //    }
        //    else
        //    {
        //        Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
        //    }
        //});

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void InitializeFirebase()
    {
        // Khởi tạo Firebase Auth và Database
        auth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Gán sự kiện cho các nút
        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(ShowSettings);
        quitButton.onClick.AddListener(QuitGame);
        quitAccountButton.onClick.AddListener(QuitAccount);
        HighScoreButton.onClick.AddListener(HighScoreTop);

        DataLevelManager.Instance.FireBaseInit();
    }

    private void NewGame()
    {
        Debug.Log("Starting a new game...");
        loginPanel.SetActive(false);
        homePanel.SetActive(false);
        AllMap.SetActive(true);
        DataLevelManager.Instance.NewGame();

        GameManager.Instance.CurrentCheckPoint = null;
        GameManager.Instance.CheckSpawnPos();

        UIManager.Instance.TutPanel.SetActive(true);
    }

    private void ContinueGame()
    {
        Debug.Log("Continuing the game...");

        loginPanel.SetActive(false);
        homePanel.SetActive(false);

        AllMap.SetActive(true);

        DataLevelManager.Instance.ContinueGame();
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
        auth.SignOut();

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
        loginPanel.SetActive(true);
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
