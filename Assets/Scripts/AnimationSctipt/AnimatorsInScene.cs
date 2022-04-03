using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsInScene : MonoBehaviour
{
    private static AnimatorsInScene instance;
    private Animator[] animators;
    public static AnimatorsInScene Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("TriggerAnimations");
                go.AddComponent<AnimatorsInScene>();
                go.GetComponent<AnimatorsInScene>().animators = GameObject.FindObjectsOfType<Animator>();
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    public Animator[] GetAnimators()
    {
        return animators;
    }
}
