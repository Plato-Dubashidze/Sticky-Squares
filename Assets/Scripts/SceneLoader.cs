using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int nextSceneCount;
    private void Start()
    {
        GlobalEventManager.LoadNextLevel.AddListener(NextSceneLoad);
    }

    private void NextSceneLoad()
    {
        nextSceneCount = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneCount < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneCount);
        else
        {
            GlobalEventManager.loadMainMenu();
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.LoadNextLevel.RemoveListener(NextSceneLoad);
    }
}
