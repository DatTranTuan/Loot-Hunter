using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeManager : MonoBehaviour
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
    public GameObject homePanel;


    [Header("Settings Panel")]
    public GameObject settingsPanel;


    [SerializeField] private GameObject map1;

    void Start()
    {
        // Khởi tạo Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Gán sự kiện cho các nút
        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
        quitAccountButton.onClick.AddListener(QuitAccount);

        // Hiển thị tên người chơi
        DisplayUserName();
    }

    private void DisplayUserName()
    {
        if (auth.CurrentUser != null)
        {
            string userId = auth.CurrentUser.UserId;

            // Lấy dữ liệu từ Firebase
            databaseReference.Child("users").Child(userId).Child("username").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error fetching username: " + task.Exception);
                    userNameText.text = "Welcome, Guest!";
                }
                else if (task.IsCompleted)
                {
                    if (task.Result.Exists)
                    {
                        string username = task.Result.Value.ToString();
                        userNameText.text = $"Welcome, {username}!";
                    }
                    else
                    {
                        Debug.LogWarning("Username does not exist in database.");
                        userNameText.text = "Welcome, Guest!";
                    }
                }
            });
        }
        else
        {
            Debug.LogWarning("No user is currently logged in.");
            userNameText.text = "Welcome, Guest!";
        }
    }

    public void NewGame()
    {
        Debug.Log("Starting a new game...");
        // Thêm logic khởi tạo game mới
        homePanel.SetActive(false);
        loginPanel.SetActive(false);
        map1.SetActive(true);
    }

    private void ContinueGame()
    {
        Debug.Log("Continuing the game...");
        // Thêm logic tiếp tục game (tải dữ liệu cũ)
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
        // Chuyển về màn hình đăng nhập
        ShowLogin();
    }

    private void ShowLogin()
    {
        // Chuyển về giao diện đăng nhập (nếu cần)
        Debug.Log("Redirecting to Login Screen...");
        loginPanel.SetActive(true);
        homePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void ShowSettings()
    {
        settingsPanel.SetActive(true);
        homePanel.SetActive(true);
    }

    private void ShowHome()
    {
        homePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
