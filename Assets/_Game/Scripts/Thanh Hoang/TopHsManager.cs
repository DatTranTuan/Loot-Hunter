using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;

public class TopHsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] playerNameTexts; 
    [SerializeField] private TMP_Text[] playerScoreTexts; 
    [SerializeField] private GameObject panelHs; 
    private DatabaseReference reference; 

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ShowTopHighScores()
    {
        panelHs.SetActive(true);
        LoadTopHighScores();
    }

    private void LoadTopHighScores()
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

                         // Update UI trên main thread
                         UnityMainThreadDispatcher.Enqueue(() =>
                         {
                             UpdateTopHighScoreUI(topScores);
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

    private void UpdateTopHighScoreUI(List<KeyValuePair<string, int>> topScores)
    {
        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            if (i < topScores.Count)
            {
                playerNameTexts[i].text = topScores[i].Key; // Cập nhật tên người chơi
                playerScoreTexts[i].text = topScores[i].Value.ToString(); // Cập nhật điểm số
            }
            else
            {
                playerNameTexts[i].text = "";
                playerScoreTexts[i].text = "";
            }
        }
    }

    private void ClearTopHighScoreUI()
    {
        foreach (var nameText in playerNameTexts)
        {
            nameText.text = "";
        }
        foreach (var scoreText in playerScoreTexts)
        {
            scoreText.text = "";
        }
    }
}
