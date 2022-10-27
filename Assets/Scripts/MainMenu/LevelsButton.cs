using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsButton : MonoBehaviour
{
    public GameObject number;
    public Sprite Lock, def;
    public AudioClip clipLock, clipUnLock;
    public AudioMixerGroup audioMixerSound;

    public int curButton;

    private bool locked;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixerSound;
        GetComponentInChildren<TextMeshProUGUI>().text = curButton.ToString();

        if(PlayerPrefs.GetInt("reachedLevel") == 0)
        {
            PlayerPrefs.SetInt("reachedLevel", 1);
        }

        if(PlayerPrefs.GetInt("reachedLevel") >= curButton)
        {
            locked = false;           
        }
        else
        {
            locked = true;
        }

        if (locked)
        {
            GetComponent<Image>().sprite = Lock;
            number.SetActive(false);
            audioSource.clip = clipLock;
        }
        else if (!locked)
        {
            GetComponent<Image>().sprite = def;
            number.SetActive(true);
            audioSource.clip = clipUnLock;
        }            
    }


    public void LoadButtonLevel()
    {
        audioSource.Play();
        StartCoroutine(LoadDelay());
    }

    private IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (!locked)
        {
            GlobalEventManager.loadLevelFromMenu();
            SceneManager.LoadScene(curButton + "_level");
        }
    }
}
