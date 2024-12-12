using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DataLevelManager : Singleton<DataLevelManager>
{
    [SerializeField] private List<GameObject> listMap = new List<GameObject>();

    private DatabaseReference databaseReference;
    private FirebaseAuth auth;
    private string userId;
    private int currentLevel;
    private int testLevel;

    private void Start()
    {
        FireBaseInit();
    }

    public void FireBaseInit()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        if (auth.CurrentUser != null)
        {
            userId = auth.CurrentUser.UserId;
            CheckCurrentLevel();
        }
        else
        {
            Debug.LogError("Người dùng chưa đăng nhập");
            userId = null;
        }
    }


    private void LoadCurrentLevel(int level)
    {
        listMap[level - 1].SetActive(true);
    }

    public async void CheckCurrentLevel()
    {
        if (string.IsNullOrEmpty(userId))
        {
            currentLevel = 1;
        }

        try
        {
            DataSnapshot snapshot = await databaseReference.Child("users").Child(userId).Child("currentLevel").GetValueAsync();
            if (snapshot.Exists)
            {
                currentLevel = int.Parse(snapshot.Value.ToString());
                Debug.Log("Level hiện tại từ Firebase: " + currentLevel);
            }
            else
            {
                Debug.Log("Khởi tạo level 1.");
                currentLevel = 1;
                SaveCurrentLevel(); 
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi tải level từ Firebase: " + e.Message);
        }
    }


    private void SaveCurrentLevel()
    {
        if (string.IsNullOrEmpty(userId) || databaseReference == null)
        {
            Debug.LogError("databaseReference chưa được khởi tạo");
            return;
        }

        databaseReference.Child("users").Child(userId).Child("currentLevel").SetValueAsync(currentLevel).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Đã lưu level: " + currentLevel);
            }
            else
            {
                Debug.LogError("Lỗi khi lưu level: " + task.Exception);
            }
        });
    }

    public void NewGame()
    {
        currentLevel = 1;

        LoadCurrentLevel(currentLevel);

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("Người dùng chưa đăng nhập");
        }
        else
        {
            SaveCurrentLevel();
        }


    }


    public void NextLevel()
    {
        listMap[currentLevel - 1].SetActive(false);

        currentLevel++;

        listMap[currentLevel - 1].SetActive(true);

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("Người dùng chưa đăng nhập");
        }
        else
        {
            SaveCurrentLevel();
        }

    }

    public void ContinueGame()
    {
        LoadCurrentLevel(currentLevel);
    }

}
