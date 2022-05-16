using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Canvas canvas;

    public Slider masterVolumeSlider;

    private float startVolValue;

    public Toggle toggleFullScreen;


    void Start()
    {
        masterVolumeSlider.value = SoundManager.Instance.volValue;
    }


    public void setVolume()
    {
        SoundManager.Instance.setSound(masterVolumeSlider.value);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void closeModal()
    {
        canvas.enabled = false;
    }


}
