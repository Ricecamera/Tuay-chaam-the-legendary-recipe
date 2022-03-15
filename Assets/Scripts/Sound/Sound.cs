using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioSource source;
    //public AudioClip clip;

    [Range(0, 1)]
    public float volume = 0.3f;

    public void SetVolume()
    {
        source.volume = source.volume - 0.05f;
    }

}
