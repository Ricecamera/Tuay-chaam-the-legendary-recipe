using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    //[SerializeField] private Sound[] sounds;

    //[SerializeField] private AudioSource soundSource;

    public float volValue;


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
        volValue = 0.5f;

    }

    // public void PlaySound(AudioClip clip)
    // {
    //     soundSource.PlayOneShot(clip);
    // }

    public void PlaySound(AudioSource source)
    {
        source.PlayOneShot(source.clip);
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
    }

    public void PlaySound(string name, Sound[] clips)
    {
        Sound temp = Array.Find(clips, clip => clip.name == name);
        temp.source.PlayOneShot(temp.source.clip);
    }

    public void setSound(float volume)
    {
        for (int i = 0; i < GameObject.Find("SoundBank").transform.childCount; i++)
        {
            GameObject.Find("SoundBank").transform.GetChild(i).GetComponent<AudioSource>().volume = volume;
        }
        SoundManager.Instance.volValue = volume;
    }

    public void setVideoSound(float volume)
    {
        GameObject.Find("SoundBank").transform.GetChild(0).GetComponent<VideoPlayer>().SetDirectAudioVolume(0, volume);
    }




}
