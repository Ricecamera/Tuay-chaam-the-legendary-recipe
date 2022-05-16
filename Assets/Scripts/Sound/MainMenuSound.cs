using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuSound : MonoBehaviour
{
    //[SerializeField] private AudioClip clip;

    [SerializeField] public Sound[] clips;
    private void Start()
    {
        // SoundManager.Instance.PlaySound(clips[0].source);
        // SoundManager.Instance.PlaySound("bgm_menu", clips);
    }

    private void Awake()
    {
        SoundManager.Instance.setSound(SoundManager.Instance.volValue);
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         clips[0].SetVolume();
    //     }
    // }

}
