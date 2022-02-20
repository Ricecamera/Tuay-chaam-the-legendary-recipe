using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private Animator transition;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) //* Main menu
            {
                Button playButton = GameObject.Find("Play").GetComponent<Button>();
                Button quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
                playButton.onClick.AddListener(LoadNextScene);
                quitButton.onClick.AddListener(ExitGame);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3) //* Character Select
            {
                Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
                Button backButton = GameObject.Find("BackButton").GetComponent<Button>();
                Button helpButton = GameObject.Find("HelpButton").GetComponent<Button>();
                startButton.onClick.AddListener(StartGame);
                backButton.onClick.AddListener(LoadPrevScene);
                helpButton.onClick.AddListener(Help);
            }
        }
    }

    public void Help()
    {
        Debug.Log("Help");
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        Debug.Log("click");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPrevScene()
    {
        Debug.Log("back");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void ExportCharacter()
    {
        // SelectCharacter.prefab = CharacterSelecter.GetCharacter();
    }

    public void StartGame()
    {
        ExportCharacter();
        LoadNextScene();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //* 3 Steps 
        //* Play Animation 
        transition.SetTrigger("Start");
        //* Wait
        yield return new WaitForSeconds(transitionTime);
        //* Load Scene
        SceneManager.LoadScene(levelIndex);
    }

}
