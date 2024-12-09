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
    [SerializeField] private DependencyStatus dependencyStatus;
    [SerializeField]private FirebaseAuth auth;
    [SerializeField] private FirebaseUser user;
    [SerializeField] private DatabaseReference reference;
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textHighScore;
    [SerializeField] private TMP_Text[] topHighScoreTexts;
    [SerializeField] private GameObject panelHs;


    private int playerScore;
    private int highScore;
    
    void Awake()
    {
        
        Debug.Log($"HighScoreUI: {highScore}");
        playerScore = 0;

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
        //LoadTopHighScores();
        playerScore++;
        if (playerScore > highScore)
        {
            highScore = playerScore;
            UpdateHighScoreInFirebase(highScore);
        }
        UpdateScoreInFirebase(playerScore);
        UpdateScoreUI();
       
    }

    public void SetActiveHighScore()
    {
        textHighScore.gameObject.SetActive(true);
    }
    //top hs
    public void SetActiveTopHs()
    {
        panelHs.gameObject.SetActive(true);
        LoadTopHighScores();
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

    void LoadTopHighScores()
    {
        reference.Child("users")
            .OrderByChild("highScore")
            .LimitToLast(5)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Exists)
                    {
                        Debug.Log($"Snapshot retrieved: {snapshot.ChildrenCount} entries.");
                        List<KeyValuePair<string, int>> topScores = new List<KeyValuePair<string, int>>();

                        foreach (var child in snapshot.Children)
                        {
                            string username = child.Child("username").Value?.ToString() ?? "Unknown";

                            int highScore = 0; 
                            if (child.HasChild("highScore") && 
                            int.TryParse(child.Child("highScore").Value?.ToString(), out int parsedScore))
                            {
                                highScore = parsedScore;
                            }

                            topScores.Add(new KeyValuePair<string, int>(username, highScore));
                            Debug.Log($"User: {username}, HighScore: {highScore}");
                        }
                        topScores.Sort((x, y) => y.Value.CompareTo(x.Value));
                        //update UI
                        UnityMainThreadDispatcher.Enqueue(() =>
                        {
                            for (int i = 0; i < topHighScoreTexts.Length; i++)
                            {
                                if (i < topScores.Count)
                                {
                                    topHighScoreTexts[i].text = $"{topScores[i].Key}: {topScores[i].Value}";
                                    
                                    Debug.Log($"UI Updated {i + 1}: {topScores[i].Key}: {topScores[i].Value}");
                                }
                                else
                                {
                                    topHighScoreTexts[i].text = ""; 
                                }
                            }
                        });
                    }
                    else
                    {
                        Debug.LogWarning("No data found for top high scores.");
                    }
                }
                else
                {
                    Debug.LogError($"Failed to retrieve top high scores: {task.Exception}");
                }
            });
    }

}