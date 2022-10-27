using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private bool isGameStartedOnce;
    private void Awake()
    {
        isGameStartedOnce = PlayerPrefs.GetInt("IsStartedOnce") != 0;
        Application.targetFrameRate = 300;
        if (!isGameStartedOnce)
        {
            PlayerPrefs.SetInt("SoundBool", true ? 1 : 0);
            PlayerPrefs.SetInt("MusicBool", true ? 1 : 0);
            PlayerPrefs.SetInt("IsStartedOnce", true ? 1 : 0);
        }
    }
}
