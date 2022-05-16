using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSound : MonoBehaviour
{
    //[SerializeField] private AudioClip clip;

    [SerializeField] public Sound[] clips;
    private void Start()
    {
        // SoundManager.Instance.PlaySound(clips[0].source);
        // SoundManager.Instance.PlaySound("bgm_battle", clips); //play BGM
    }

    private void Awake()
    {
        SoundManager.Instance.setSound(SoundManager.Instance.volValue);
    }
    public void PlaySoundByName(string name)
    {
        SoundManager.Instance.PlaySound(name, clips);
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         clips[0].SetVolume();
    //     }
    // }

}
