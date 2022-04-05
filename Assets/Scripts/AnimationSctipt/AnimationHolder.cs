using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AnimatorsInScene.Instance == null)
        {
            AnimatorsInScene.Instance.Awake();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
