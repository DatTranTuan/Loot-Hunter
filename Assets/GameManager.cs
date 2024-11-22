using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
