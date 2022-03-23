using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Sound[] clips;

    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound("button", clips);
    }

    public void PlaySoundByName(string name) {
        SoundManager.Instance.PlaySound(name,clips);
    }

}
