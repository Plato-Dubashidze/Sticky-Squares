using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup musicMixerGroup, soundEffectsMixerGroup;
    public AudioClip mainMenuMusic, gameMusic, endLevelMusic;
    private static bool isAlreadyExist;
    private static bool Sound, Music;
    private AudioSource menuMusicSource, gameMusicSource, endLevelMusicSource;
    public static bool sound
    {
        get
        {
            return Sound;
        }
        set
        {
            Sound = value;
            PlayerPrefs.SetInt("SoundBool", (Sound ? 1 : 0));            
        }
    }
    public static bool music
    {
        get
        {
            return Music;
        }
        set
        {
            Music = value;
            PlayerPrefs.SetInt("MusicBool", (Music ? 1 : 0));
        }
    }
    private void Awake()
    {
        if (isAlreadyExist)
        {
            Destroy(gameObject);
        }
        isAlreadyExist = true;
        DontDestroyOnLoad(gameObject);
        AddListeners();
        menuMusicSource = gameObject.AddComponent<AudioSource>();
        gameMusicSource = gameObject.AddComponent<AudioSource>();
        endLevelMusicSource = gameObject.AddComponent<AudioSource>();

        menuMusicSource.outputAudioMixerGroup = musicMixerGroup;
        gameMusicSource.outputAudioMixerGroup = musicMixerGroup;
        endLevelMusicSource.outputAudioMixerGroup = musicMixerGroup;

        menuMusicSource.loop = true;
        gameMusicSource.loop = true;
        endLevelMusicSource.loop = true;

        sound = PlayerPrefs.GetInt("SoundBool") != 0;
        music = PlayerPrefs.GetInt("MusicBool") != 0;

    }
    private void Start()
    {
        menuMusicSource.clip = mainMenuMusic;
        menuMusicSource.volume = 0;
        menuMusicSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(menuMusicSource, 0.1f, 1f));
    }

    private void Update()
    {
        checkMusicState();
    }

    private void LoadLevelFromMenu()
    {
        gameMusicSource.clip = gameMusic;
        endLevelMusicSource.clip = endLevelMusic;
        gameMusicSource.volume = 0;
        endLevelMusicSource.volume = 0;
        gameMusicSource.Play();
        endLevelMusicSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(menuMusicSource, 0.1f, 0f));
        StartCoroutine(FadeAudioSource.StartFade(gameMusicSource, 0.1f, 1f));
    }

    private void EndLevel()
    {
        StartCoroutine(FadeAudioSource.StartFade(gameMusicSource, 0.5f, 0f));
        StartCoroutine(FadeAudioSource.StartFade(endLevelMusicSource, 0.5f, 1f));
    }

    private void LoadNextLevel()
    {
        StartCoroutine(FadeAudioSource.StartFade(endLevelMusicSource, 0.5f, 0f));
        StartCoroutine(FadeAudioSource.StartFade(gameMusicSource, 0.5f, 1f));
    }
    private void LoadMainMenu()
    {
        menuMusicSource.time = 13.71f;
        StartCoroutine(FadeAudioSource.StartFade(endLevelMusicSource, 0.1f, 0f));
        StartCoroutine(FadeAudioSource.StartFade(gameMusicSource, 0.1f, 0f));
        gameMusicSource.Stop();
        endLevelMusicSource.Stop();
        StartCoroutine(FadeAudioSource.StartFade(menuMusicSource, 0.1f, 1f));
    }

    private void AddListeners()
    {
        GlobalEventManager.LoadLevelFromMenu.AddListener(LoadLevelFromMenu);
        GlobalEventManager.EndLevel.AddListener(EndLevel);
        GlobalEventManager.LoadNextLevel.AddListener(LoadNextLevel);
        GlobalEventManager.LoadMainMenu.AddListener(LoadMainMenu);
    }


    private void checkMusicState()
    {
        if (music)
        {
            musicMixerGroup.audioMixer.SetFloat("Music Volume", -10);
        }
        else if (!music)
        {
            musicMixerGroup.audioMixer.SetFloat("Music Volume", -80);
        }

        if (sound)
        {
            soundEffectsMixerGroup.audioMixer.SetFloat("Sound Volume", -10);
        }
        else if (!sound)
        {
            soundEffectsMixerGroup.audioMixer.SetFloat("Sound Volume", -80);
        }
    }


}
