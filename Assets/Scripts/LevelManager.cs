using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int curLevel;

    private void Start()
    {
        string resultString = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
        curLevel = Int32.Parse(resultString);
        GlobalEventManager.EndLevel.AddListener(EndLevel);
    }

    private void EndLevel()
    {
        if (PlayerPrefs.GetInt("reachedLevel") <= curLevel)
        {
            PlayerPrefs.SetInt("reachedLevel", curLevel + 1);            
        }
    }
}
