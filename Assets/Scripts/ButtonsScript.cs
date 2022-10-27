using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public GameObject menuCanvas, endLevelCanvas;
    public AudioClip buttonSound;
    public AudioMixerGroup audioMixerSound;

    private AudioSource audioSource;

    private bool isLevelEnded;
    private void Start()
    {
        isLevelEnded = false;
        GlobalEventManager.EndLevel.AddListener(LevelEnd);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixerSound;
        audioSource.clip = buttonSound;
    }

    private void LevelEnd()
    {
        isLevelEnded = true;
        endLevelCanvas.SetActive(true);
    }

    public void Restart()
    {
        audioSource.Play();
        if (!isLevelEnded)
        {
            StartCoroutine(LoadDelay(null, SceneManager.GetActiveScene().buildIndex));
        }
    }

    public void ShowMenuButton()
    {
        audioSource.Play();
        if (!isLevelEnded)
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }

    public void NextLevelButton()
    {
        audioSource.Play();
        StartCoroutine(LoadDelayForNext());
    }

    public void MainMenuButton()
    {
        audioSource.Play();
        GlobalEventManager.loadMainMenu();
        StartCoroutine(LoadDelay("MainMenu", 0));
    }

    private void OnDestroy()
    {
        GlobalEventManager.EndLevel.RemoveListener(LevelEnd);
    }

    private IEnumerator LoadDelay(string sceneName, int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        if(sceneName != null)
        SceneManager.LoadScene(sceneName);
        if (sceneIndex != 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private IEnumerator LoadDelayForNext()
    {
        yield return new WaitForSeconds(0.5f);
        if (isLevelEnded)
        {
            GlobalEventManager.loadNextLevel();
        }
    }
}
