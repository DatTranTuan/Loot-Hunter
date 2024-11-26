using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
        LoadHighScoreFromFirebase();
        UpdateScoreUI();
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

    // Add Score & HighScore
    public void AddScore()
    {
        playerScore++; 
        if (playerScore > highScore) 
        {
            highScore = playerScore;
            UpdateHighScoreInFirebase(highScore); 
        }

        UpdateScoreUI(); 
        UpdateScoreInFirebase(playerScore);
    }
    // UI
    void UpdateScoreUI()
    {
        if (textScore != null)
            textScore.text = $"Score: {playerScore}";

        if (textHighScore != null)
            textHighScore.text = $"High Score: {highScore}";
    }

    // Save current score on firebase
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
        if (user != null)
        {
            string userId = user.UserId;

            reference.Child("users").Child(userId).Child("highScore").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    highScore = int.Parse(task.Result.Value.ToString());
                    Debug.Log($"Loaded High Score: {highScore}");
                }
                else
                {
                    Debug.LogError("Failed to load High Score or High Score does not exist.");
                }

                UpdateScoreUI();
            });

            reference.Child("users").Child(userId).Child("score").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    playerScore = int.Parse(task.Result.Value.ToString());
                    Debug.Log($"Loaded Player Score: {playerScore}");
                    UpdateScoreUI();
                }
                else
                {
                    Debug.LogError("Failed to load Player Score or Player Score does not exist.");
                }
            });
        }
    }

    void OnApplicationQuit()
    {
        UpdateScoreInFirebase(playerScore);
        UpdateHighScoreInFirebase(highScore);
    }


}
