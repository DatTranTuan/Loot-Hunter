using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseReference databaseReference;
    public HomeManager home;

    [Header("Register")]
    public Button regis_btn;
    public TMP_InputField regisusernameField;
    public TMP_InputField regisemailField;
    public TMP_InputField regispasswordField;
    public TMP_InputField regisconfirmPasswordField;
    public GameObject registerPanel;


    [Header("Login")]
    public Button login_btn;
    public TMP_InputField loginemailField;
    public TMP_InputField loginpasswordField;
    public GameObject loginPanel;


    [Header("Forgot")]
    public Button forgot_btn;
    public TMP_InputField forgotemailField;
    public GameObject forgotPanel;
    public GameObject confirmPanel;
    

    [Header("Home")]
    public GameObject homePanel;
    public GameObject settingsPanel;
    public TMP_Text userNameText;

    [Header("Hide/Show Pass")]
    public Image toggleIcon;
    public Sprite showPasswordIcon;
    public Sprite hidePasswordIcon;
    public Image toggleIcon2;
    public Sprite showPasswordIcon2;
    public Sprite hidePasswordIcon2;
    public Image toggleIcon3;
    public Sprite showPasswordIcon3;
    public Sprite hidePasswordIcon3;
    [SerializeField] private TMP_Text loginStatusText;
    [SerializeField] private TMP_Text registerStatusText;
    private bool isPasswordVisible = false;

    void Start()
    {
        ShowLogin();
        // Khởi tạo Firebase Auth
        auth = FirebaseAuth.DefaultInstance;

        // Kiểm tra và thiết lập các dependencies của Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Firebase đã sẵn sàng
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized successfully!");
            }
            else
            {
                // Xử lý lỗi nếu Firebase không khởi tạo được
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });

        // Gán sự kiện cho các nút
        regis_btn.onClick.AddListener(RegisterUser);
        login_btn.onClick.AddListener(LoginUser);
        forgot_btn.onClick.AddListener(ForgotPass);
    }

    private void activeNotiLogin()
    {
        loginStatusText.gameObject.SetActive(true);
        loginStatusText.color = Color.red;
    }

    private void activeNotiRegister()
    {
        registerStatusText.gameObject.SetActive(true);
        registerStatusText.color = Color.red;
    }
    public void LoginUser()
    {
        string email = loginemailField.text;
        string password = loginpasswordField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            loginStatusText.text = "Email và mật khẩu không được để trống!";
           
            activeNotiLogin();
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                FirebaseException firebaseEx = task.Exception?.Flatten().InnerExceptions[0] as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx?.ErrorCode;

                switch (errorCode)
                {
                    case AuthError.InvalidEmail:
                        loginStatusText.text = "Email không hợp lệ.";
                        activeNotiLogin();
                        break;
                    case AuthError.WrongPassword:
                        loginStatusText.text = "Mật khẩu không chính xác.";
                        activeNotiLogin();
                        break;
                    case AuthError.UserNotFound:
                        loginStatusText.text = "Người dùng không tồn tại.";
                        activeNotiLogin();
                        break;
                    default:
                        loginStatusText.text = "Đăng nhập thất bại. Vui lòng thử lại.";
                        activeNotiLogin();
                        break;
                }
            }
            else
            {
                loginStatusText.text = "Đăng nhập thành công!";
                activeNotiLogin();
                ShowHome();
                DisplayUserName();
            }
        });
    }

    // Kiểm tra định dạng email
    private bool IsValidEmail2(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void RegisterUser()
    {
        string username = regisusernameField.text;
        string email = regisemailField.text;
        string password = regispasswordField.text;
        string confirmPassword = regisconfirmPasswordField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            registerStatusText.text = "Vui lòng điền đầy đủ thông tin.";
            activeNotiRegister();
            return;
        }

        if (password != confirmPassword)
        {
            registerStatusText.text = "Mật khẩu không khớp.";
            activeNotiRegister();
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                FirebaseException firebaseEx = task.Exception?.Flatten().InnerExceptions[0] as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx?.ErrorCode;

                switch (errorCode)
                {
                    case AuthError.EmailAlreadyInUse:
                        registerStatusText.text = "Email đã được sử dụng.";
                        activeNotiRegister();
                        break;
                    case AuthError.InvalidEmail:
                        registerStatusText.text = "Email không hợp lệ.";
                        activeNotiRegister();
                        break;
                    case AuthError.WeakPassword:
                        registerStatusText.text = "Mật khẩu quá yếu.";
                        activeNotiRegister();
                        break;
                    default:
                        registerStatusText.text = "Đăng ký thất bại. Vui lòng thử lại.";
                        activeNotiRegister();
                        break;
                }
            }
            else
            {
               
                FirebaseUser newUser = task.Result.User;
                UserProfile profile = new UserProfile { DisplayName = username };

                newUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(updateTask =>
                {
                    if (updateTask.IsCompleted)
                    {
                        databaseReference.Child("users").Child(newUser.UserId).Child("username").SetValueAsync(username);
                        registerStatusText.text = "Đăng ký thành công!";
                        activeNotiRegister();
                        ShowLogin();
                       
                    }
                });
            }
        });
    }


    // Hàm kiểm tra định dạng email
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
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
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists && snapshot.Value != null)
                    {
                        string username = snapshot.Value.ToString().Trim();
                        
                        if (!string.IsNullOrEmpty(username))
                        {
                            Debug.Log($"Username fetched: {username}");
                            userNameText.text = $"Welcome, {username}!";
                        }
                        else
                        {
                            Debug.LogWarning("Username is empty in the database.");
                            userNameText.text = "Welcome, Guest!";
                        }
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

    private void SaveUserData(string userId, string username, string email)
    {
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        var userData = new Dictionary<string, object>
        {
            { "username", username },
            { "email", email }
        };


        // Lưu dữ liệu vào child "users/{userId}/username"
        databaseReference.Child("users").Child(userId).SetValueAsync(userData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Failed to save username: Task was canceled.");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError($"Failed to save username: {task.Exception}");
            }
            else
            {
                Debug.Log("Username saved successfully.");
            }
        });
    }


    public void ForgotPass()
    {
        string forgotemail = forgotemailField.text;

        auth.SendPasswordResetEmailAsync(forgotemail).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SendPasswordResetEmailAsync canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("SendPasswordResetEmailAsync failed");
            }
            if (task.IsCompleted)
            {
                Debug.Log("SendPasswordResetEmailAsync successful");

            }
        });
    } 


    //Chuyển màn hình
    public void ShowLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(false);
        confirmPanel.SetActive(false);
        homePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ShowForgot()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(true);
    }

    public void ShowConfirm()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void ShowHome()
    {
        homePanel.SetActive(true);
    }
 

    //Hiển thị mật khẩu
    public void TogglePasswordVisibility()
    {
        // Chuyển đổi trạng thái hiển thị/ẩn
        isPasswordVisible = !isPasswordVisible;

        // Cập nhật chế độ hiển thị mật khẩu
        regispasswordField.contentType = isPasswordVisible ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
        regispasswordField.ForceLabelUpdate(); // Cập nhật giao diện InputField

        // Cập nhật hình ảnh của nút
        toggleIcon.sprite = isPasswordVisible ? hidePasswordIcon : showPasswordIcon;
    }
    public void ToggleLoginPasswordVisibility()
    {
        // Chuyển đổi trạng thái hiển thị/ẩn
        isPasswordVisible = !isPasswordVisible;

        // Cập nhật chế độ hiển thị mật khẩu
        loginpasswordField.contentType = isPasswordVisible ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
        loginpasswordField.ForceLabelUpdate(); // Cập nhật giao diện InputField

        // Cập nhật hình ảnh của nút
        toggleIcon3.sprite = isPasswordVisible ? hidePasswordIcon3 : showPasswordIcon3;
    }

    public void ToggleConfirmPasswordVisibility()
    {
        // Chuyển đổi trạng thái hiển thị/ẩn
        isPasswordVisible = !isPasswordVisible;

        // Cập nhật chế độ hiển thị mật khẩu
        regisconfirmPasswordField.contentType = isPasswordVisible ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
        regisconfirmPasswordField.ForceLabelUpdate(); // Cập nhật giao diện InputField

        // Cập nhật hình ảnh của nút
        toggleIcon2.sprite = isPasswordVisible ? hidePasswordIcon2 : showPasswordIcon2;
    }

}