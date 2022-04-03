using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //! These buttons will be used in the future
    private Button newGameButton;
    private Button loadGameButton;
    private Button quitButton;
    private Button settingsButton;

    private void Start()
    {
        //! These buttons will be used in the future
        newGameButton = GameObject.Find("New Game").GetComponent<Button>();
        //loadGameButton = GameObject.Find("Load Game").GetComponent<Button>();
        settingsButton = GameObject.Find("Setting").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //! These buttons will be used in the future
            newGameButton.onClick.AddListener(SceneLoader.Instance.LoadNextScene);
            //loadGameButton.onClick.AddListener(SceneLoader.Instance.LoadNextScene);
            quitButton.onClick.AddListener(() =>
            {
                Debug.Log("Quit");
                Application.Quit();
            });
        }
    }
}
