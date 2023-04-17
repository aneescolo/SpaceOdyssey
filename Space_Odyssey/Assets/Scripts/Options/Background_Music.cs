using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_Music : MonoBehaviour
{
    private static Background_Music instance;

    [SerializeField] private AudioSource mainaudio_Source;

    [SerializeField] private AudioClip background_music;
    [SerializeField] private AudioClip ambiance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void PlaySoundMusic(AudioClip clip)
    {
        mainaudio_Source.PlayOneShot(clip);
    }
}
