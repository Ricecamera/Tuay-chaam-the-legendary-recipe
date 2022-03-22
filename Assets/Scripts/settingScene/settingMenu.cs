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

    public void OnEnabled()
    {
        audioMixer.GetFloat("volume", out startVolValue);
        masterVolumeSlider.value = startVolValue;

    }
    public void setVolume()
    {
        audioMixer.SetFloat("volume", masterVolumeSlider.value);
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
