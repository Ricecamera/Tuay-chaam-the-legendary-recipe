using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    //[SerializeField] private Sound[] sounds;

    //[SerializeField] private AudioSource soundSource;

    public float startVolValue;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        startVolValue = 0.5f;
    }

    // public void PlaySound(AudioClip clip)
    // {
    //     soundSource.PlayOneShot(clip);
    // }

    public void PlaySound(AudioSource source)
    {
        source.volume = startVolValue;
        source.PlayOneShot(source.clip);
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
    }

    public void PlaySound(string name, Sound[] clips)
    {
        Sound temp = Array.Find(clips, clip => clip.name == name);
        temp.source.volume = startVolValue;
        temp.source.PlayOneShot(temp.source.clip);
    }


    

}
