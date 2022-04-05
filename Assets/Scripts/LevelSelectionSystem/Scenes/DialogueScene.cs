using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene : MonoBehaviour
{
    [SerializeField] private GameObject dialogueHolder;
    private Animator tonhom;
    private Animator chaam;

    void Start()
    {
        for (int i = 0; i < AnimatorsInScene.Instance.GetAnimators().Length; i++)
        {
            if (AnimatorsInScene.Instance.GetAnimators()[i].name == "Tonhom")
            {
                tonhom = AnimatorsInScene.Instance.GetAnimators()[i];
                Debug.Log(tonhom.name);
            }
            if (AnimatorsInScene.Instance.GetAnimators()[i].name == "BrokenChaam")
            {
                chaam = AnimatorsInScene.Instance.GetAnimators()[i];
                Debug.Log(chaam.name);
            }
        }
    }

    void Update()
    {
        if (dialogueHolder.transform.GetChild(2).gameObject.activeSelf == true)
        {
            tonhom.SetTrigger("Come_in");
            chaam.SetTrigger("Come_out");
        }

    }
}
