using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System.Collections;

public class DataLevelManager : Singleton<DataLevelManager>
{
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject[] levelPrefabs;
    private GameObject currentPlayer;

    private FirebaseAuth auth;
    private FirebaseUser user;
    private DatabaseReference reference;

    private void Start()
    {
        InitializeFirebase();
        StartCoroutine(LoadLevelFromFirebase());  
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

 
    public void SaveLevelToFirebase(int level)
    {
        if (user == null) return;

        string userId = user.UserId;
        reference.Child("users").Child(userId).Child("currentLevel").SetValueAsync(level).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Successfully saved level {level} to Firebase.");
            }
            else
            {
                Debug.LogError("Failed to save level to Firebase.");
            }
        });
    }


    private IEnumerator LoadLevelFromFirebase()
    {
        while (user == null) 
        {
            yield return null;
        }

            reference.Child("users")
            .Child(user.UserId)
            .Child("currentLevel")
            .GetValueAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    int currentLevel = int.Parse(task.Result.Value.ToString());
                    Debug.Log($"Loaded Level {currentLevel} from Firebase.");
                    LoadLevel(currentLevel);
                }
                else
                {
                    Debug.LogWarning("No saved level found. Defaulting to Level 1.");
                    SaveLevelToFirebase(1);
                    LoadLevel(1);
                }
            });
    }


    private void LoadLevel(int level)
    {
        foreach (Transform child in levelParent)
        {
            Destroy(child.gameObject);
        }

       
        if (level > 0 && level <= levelPrefabs.Length)
        {
            GameObject loadedLevel = Instantiate(levelPrefabs[level - 1], levelParent);
            Debug.Log($"Level {level} loaded.");

         
            StartPosManager startPosManager = loadedLevel.GetComponentInChildren<StartPosManager>();
            if (startPosManager != null)
            {
                Vector3 startPosition = startPosManager.GetStartPosition();
                SetPlayerPos(startPosition);
            }
            else
            {
                Debug.LogError("No StartPosManager found in the level prefab.");
            }
        }
        else
        {
            Debug.LogError($"Invalid level index: {level}. Check your prefab list.");
        }
    }

    
    public void PlayerCompletedLevel()
    {
        int nextLevel = GetCurrentLevel() + 1;

        if (nextLevel <= levelPrefabs.Length)
        {
            SaveLevelToFirebase(nextLevel);
            LoadLevel(nextLevel);
        }
        else
        {
            Debug.Log("Player has completed all available levels!");
        }
    }

    
    private int GetCurrentLevel()
    {
        foreach (Transform child in levelParent)
        {
            for (int i = 0; i < levelPrefabs.Length; i++)
            {
                if (child.name.Contains(levelPrefabs[i].name))
                {
                    return i + 1;
                }
            }
        }
        return 1;
    }

    private void SetPlayerPos(Vector3 position)
    {
       PlayerControl.Instance.transform.position = position;
    }
}
