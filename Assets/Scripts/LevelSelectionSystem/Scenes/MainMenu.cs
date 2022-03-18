using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    //! These buttons will be used in the future
    // private Button newGameButton;
    // private Button loadGameButton;
    private Button playButton;
    private Button quitButton;

    private void Start() {
        //! These buttons will be used in the future
        // newGameButton = GameObject.Find("NewGame").GetComponent<Button>();
        // loadGameButton = GameObject.Find("LoadGame").GetComponent<Button>();
        playButton = GameObject.Find("Play").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            //! These buttons will be used in the future
            // newGameButton.onClick.AddListener(LoadNextScene);
            // loadGameButton.onClick.AddListener(LoadPrevScene);
            playButton.onClick.AddListener(SceneLoader.Instance.LoadNextScene);
            quitButton.onClick.AddListener(() => {
                Debug.Log("Quit");
                Application.Quit(); 
            });
        }
    }
}
