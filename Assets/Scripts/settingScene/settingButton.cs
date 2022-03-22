using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingButton : MonoBehaviour
{
    public Canvas settingCanvas;

    public void OnEnable()
    {
        settingCanvas.enabled = false;
    }
    public void openSetting()
    {
        settingCanvas.enabled = true;
    }
}
