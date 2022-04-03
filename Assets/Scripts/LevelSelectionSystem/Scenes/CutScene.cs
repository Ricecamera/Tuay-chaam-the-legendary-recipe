using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    private Button skipButton;

    private void Start()
    {
        skipButton = GameObject.Find("Skip").GetComponent<Button>();
    }

    private void Update(){ 
        if (Input.GetMouseButtonDown(0)){
            skipButton.onClick.AddListener(SceneLoader.Instance.LoadNextScene);
        }
    }
}
