using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            CharacterSelecter.instance.chaam = null;
            CharacterSelecter.instance.characters = new List<ItemObject>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startButton.onClick.AddListener(() =>
            {
                if (CharacterSelecter.instance.GetChaam() == null || CharacterSelecter.instance.GetCharacters().Count < 1)
                {
                    CharacterSelecter.instance.ShowPopup();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 12)
                {
                    // this scene is cook system tutorial
                    SceneLoader.Instance.LoadSceneByIndex(6); // Load battle scene
                }
                else
                {
                    SceneLoader.Instance.LoadNextScene(); // Load battle scene
                }
            });
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
