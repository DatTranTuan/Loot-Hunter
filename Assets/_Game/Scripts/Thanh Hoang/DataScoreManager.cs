using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataScoreManager : Singleton<DataScoreManager>
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference reference;
    public TMP_Text textScore;
    public TMP_Text textHighScore;

    private int playerScore;
    private int highScore;

    void Start()
    {
        Debug.Log($"HighScoreUI: {highScore}");
        playerScore = 0;
        LoadHighScoreFromFirebase();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError($"Could not  resolve all Firebase dependencies: {dependencyStatus}");
            }
        });

    }

    void InitializeFirebase()
    {

        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;


        if (auth.CurrentUser == null)
        {
            auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    user = auth.CurrentUser;
                    Debug.Log($"Signed in as: {user.UserId}");
                    LoadHighScoreFromFirebase();
                }
                else
                {
                    Debug.LogError("Failed to sign in anonymously.");
                }
            });
        }
        else
        {
            user = auth.CurrentUser;
            Debug.Log($"Already signed in as: {user.UserId}");
            LoadHighScoreFromFirebase();

        }
    }

    public void AddScore()
    {
        playerScore++;
        if (playerScore > highScore)
        {
            highScore = playerScore;
            UpdateHighScoreInFirebase(highScore);
        }
        UpdateScoreInFirebase(playerScore);
        UpdateScoreUI();
    }
    // UI
    void UpdateScoreUI()
    {
        Debug.Log($"Updating UI -> Current Score: {playerScore}, High Score: {highScore}");
        if (textScore != null)
            textScore.text = $"Score: {playerScore}";

        if (textHighScore != null)
            textHighScore.text = $"High Score: {highScore}";
    }

    // Save score on firebase
    void UpdateScoreInFirebase(int score)
    {
        if (user != null)
        {
            string userId = user.UserId;
            reference.Child("users").Child(userId).Child("score").SetValueAsync(score).ContinueWith(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("Score updated successfully.");
                else
                    Debug.LogError("Failed to update score.");
            });
        }
    }

    // Save HighScore on Firebase
    void UpdateHighScoreInFirebase(int highScore)
    {
        if (user != null)
        {
            string userId = user.UserId;
            reference.Child("users").Child(userId).Child("highScore").SetValueAsync(highScore).ContinueWith(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("High Score updated successfully.");
                else
                    Debug.LogError("Failed to update High Score.");
            });
        }
    }
    void LoadHighScoreFromFirebase()
    {
       
        if (user == null) return;

        string userId = user.UserId;

        reference.Child("users").Child(userId).Child("highScore").GetValueAsync().ContinueWith(task =>
        {
            if (!task.IsCompleted)
            {
                Debug.LogError("Failed to load High Score.");
                return;
            }

            if (task.Result.Exists)
            {
                highScore = int.Parse(task.Result.Value.ToString());
                Debug.Log($"Loaded High Score: {highScore}");
            }
            else
            {
                highScore = 0;
                Debug.LogWarning("High Score does not exist. Initializing with 0.");
                UpdateHighScoreInFirebase(highScore);
            }
            Debug.Log($"Update UI High Score: {highScore}");
            UpdateScoreUI();
        });
    }

    void OnApplicationQuit()
    {
        UpdateScoreInFirebase(playerScore);
        UpdateHighScoreInFirebase(highScore);
    }
}