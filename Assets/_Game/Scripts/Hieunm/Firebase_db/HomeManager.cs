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

    public GameObject datPanel;

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
        settingsButton.onClick.AddListener(ShowSettings);
        quitButton.onClick.AddListener(QuitGame);
        quitAccountButton.onClick.AddListener(QuitAccount);

    }

    private void NewGame()
    {
        Debug.Log("Starting a new game...");
        // Thêm logic khởi tạo game mới
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

        // Làm sạch dữ liệu tạm thời
        ClearUserNameDisplay();
        ShowLogin();
    }

    private void ShowHome()
    {
        settingsPanel.SetActive(false);
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

}
