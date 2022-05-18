using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoSound : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SoundManager.Instance.setVideoSound(SoundManager.Instance.volValue);
    }

}
