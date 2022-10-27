using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundButton : MonoBehaviour
{
    public SpriteRenderer on, off;
    public enum Type
    {
        Sound,
        Music
    };

    public Type type;

    public AudioClip clipOff, clipOn;

    public AudioMixerGroup audioMixerSound;

    private AudioSource clipOffSource, clipOnSource;

    private void Start()
    {
        InitiateState();
        if(type == Type.Sound)
        {
            clipOffSource = gameObject.AddComponent<AudioSource>();
            clipOnSource = gameObject.AddComponent<AudioSource>();
            clipOnSource.outputAudioMixerGroup = audioMixerSound;
            clipOffSource.outputAudioMixerGroup = audioMixerSound;
            clipOnSource.clip = clipOn;
            clipOffSource.clip = clipOff;
            clipOnSource.playOnAwake = false;
            clipOffSource.playOnAwake = false;
            clipOnSource.volume = 0.3f;
            clipOffSource.volume = 0.3f;
        }
    }

    public void onCLick()
    {
        switch (type) 
        { 
            case Type.Sound:
                AudioManager.sound = !AudioManager.sound;
                if (AudioManager.sound)
                {
                    clipOnSource.Play();
                    on.enabled = true;
                    off.enabled = false;
                }
                else
                {
                    clipOffSource.Play();
                    on.enabled = false;
                    off.enabled = true;
                }
                break;
            case Type.Music:
                AudioManager.music = !AudioManager.music;
                if (AudioManager.music)
                {
                    on.enabled = true;
                    off.enabled = false;
                }
                else
                {
                    on.enabled = false;
                    off.enabled = true;
                }
                break;
        }
    }

    private void InitiateState()
    {
        switch (type)
        {
            case Type.Sound:
                if (AudioManager.sound)
                {
                    on.enabled = true;
                    off.enabled = false;
                }
                else
                {
                    on.enabled = false;
                    off.enabled = true;
                }
                break;
            case Type.Music:
                if (AudioManager.music)
                {
                    on.enabled = true;
                    off.enabled = false;
                }
                else
                {
                    on.enabled = false;
                    off.enabled = true;
                }
                break;
        }
    }
}
