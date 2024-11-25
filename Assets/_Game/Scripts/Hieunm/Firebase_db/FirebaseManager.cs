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


    public void LoginUser()
    {
        string loginemail = loginemailField.text;
        string loginpass = loginpasswordField.text;

        // Kiểm tra email
        if (string.IsNullOrEmpty(loginemail))
        {
            Debug.Log("Email is empty");
            return; // Dừng lại nếu email trống
        }

        // Kiểm tra định dạng email
        if (!IsValidEmail2(loginemail))
        {
            Debug.Log("Invalid email format");
            return; // Dừng lại nếu email không hợp lệ
        }

        // Kiểm tra mật khẩu
        if (string.IsNullOrEmpty(loginpass))
        {
            Debug.Log("Password is empty");
            return; // Dừng lại nếu mật khẩu trống
        }

        // Tiến hành đăng nhập người dùng

        auth.SignInWithEmailAndPasswordAsync(loginemail, loginpass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Login canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Login failed");
            }
            if (task.IsCompleted)
            {
                Debug.Log("Login successful");
                FirebaseUser user = task.Result.User;

                ShowHome();
            }
        });
    }

    // Hàm kiểm tra định dạng email
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
        string regisusername = regisusernameField.text;
        string regisemail = regisemailField.text;
        string regispassword = regispasswordField.text;
        string regisconfirmpassword = regisconfirmPasswordField.text;

        // Kiểm tra tên người dùng
        if (string.IsNullOrEmpty(regisusername))
        {
            Debug.Log("Username is empty");
            return; // Dừng lại nếu tên người dùng trống
        }

        // Kiểm tra email
        if (string.IsNullOrEmpty(regisemail))
        {
            Debug.Log("Email is empty");
            return; // Dừng lại nếu email trống
        }

        // Kiểm tra định dạng email
        if (!IsValidEmail(regisemail))
        {
            Debug.Log("Invalid email format");
            return; // Dừng lại nếu email không hợp lệ
        }

        // Kiểm tra mật khẩu
        if (string.IsNullOrEmpty(regispassword))
        {
            Debug.Log("Password is empty");
            return; // Dừng lại nếu mật khẩu trống
        }

        // Kiểm tra xác nhận mật khẩu
        if (regispassword != regisconfirmpassword)
        {
            Debug.LogError("Password does not match");
            return; // Dừng lại nếu mật khẩu không khớp
        }

        // Kiểm tra độ dài mật khẩu
        if (regispassword.Length < 6)
        {
            Debug.Log("Password must be at least 6 characters long");
            return; // Dừng lại nếu mật khẩu ngắn hơn 6 ký tự
        }

        // Tiến hành đăng ký người dùng
        auth.CreateUserWithEmailAndPasswordAsync(regisemail, regispassword).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Registration canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("Registration failed");
                }
                if (task.IsCompleted)
                {
                    Debug.Log("Registration successful");
                    FirebaseUser newUser = task.Result.User;
                    SaveUserData(newUser.UserId, regisusername, regisemail);
                    ShowLogin();
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