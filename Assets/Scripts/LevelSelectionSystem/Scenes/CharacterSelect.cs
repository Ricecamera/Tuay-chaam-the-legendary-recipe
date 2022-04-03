using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
    private Button startButton;
    private Button backButton;
    private Button helpButton;
    private void Start()
    {
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        //helpButton = GameObject.Find("HelpButton").GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startButton.onClick.AddListener(SceneLoader.Instance.LoadNextScene);
            backButton.onClick.AddListener(SceneLoader.Instance.LoadPrevScene);
            // helpButton.onClick.AddListener(Help);
        }
    }

    public void Help()
    {
        //TODO add help tooltips
        Debug.Log("Help");
    }
}
