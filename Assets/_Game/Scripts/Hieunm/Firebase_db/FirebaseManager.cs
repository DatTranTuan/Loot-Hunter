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
    public GameObject homePanel;
    public GameObject settingsPanel;

    [Header("Hide/Show Pass")]
    private bool isPasswordVisible = false;
    public Image toggleIcon; 
    public Sprite showPasswordIcon;
    public Sprite hidePasswordIcon;
    public Image toggleIcon2;
    public Sprite showPasswordIcon2; 
    public Sprite hidePasswordIcon2;
    public Image toggleIcon3;
    public Sprite showPasswordIcon3;
    public Sprite hidePasswordIcon3;


    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        regis_btn.onClick.AddListener(RegisterUser);
        login_btn.onClick.AddListener(LoginUser);
        forgot_btn.onClick.AddListener(ForgotPass);
       
    }

   
    public void LoginUser()
    {
        string loginemail = loginemailField.text;
        string loginpass = loginpasswordField.text;

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

    public void RegisterUser()
    {
        string regisusername = regisusernameField.text;
        string regisemail = regisemailField.text;
        string regispassword = regispasswordField.text;
        string regisconfirmpassword = regisconfirmPasswordField.text;

        if (regisusername == "")
        {
            Debug.Log("Username is empty");
        }
        else if (regisemail == "")
        {
            Debug.Log("Email is empty");
        }        
        else if (regispassword != regisconfirmpassword)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
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
                    SaveUsername(newUser.UserId, regisusername);
                }

            });
        }

        
    }
    private void SaveUsername(string userId, string username)
    {
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Lưu dữ liệu vào child "users/{userId}/username"
        databaseReference.Child("users").Child(userId).Child("username").SetValueAsync(username).ContinueWithOnMainThread(task =>
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
        settingsPanel.SetActive(false);
    }
    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
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
        toggleIcon.sprite = isPasswordVisible ? hidePasswordIcon : showPasswordIcon;
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