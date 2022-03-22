using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private Animator transition;
    // Update is called once per frame

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
            if (SceneManager.GetActiveScene().buildIndex == 2) //* Character Select
            {
                Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
                Button backButton = GameObject.Find("BackButton").GetComponent<Button>();
                Button helpButton = GameObject.Find("HelpButton").GetComponent<Button>();
                startButton.onClick.AddListener(LoadNextScene);
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
        StartCoroutine(LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPrevScene()
    {
        Debug.Log("back");
        StartCoroutine(LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadSpecificScene(string sceneName)
    {

        StartCoroutine(LoadLevelByName(sceneName));
    }

    public void StartGame()
    {
        LoadNextScene();
    }

    IEnumerator LoadLevelByIndex(int levelIndex)
    {
        //* 3 Steps 
        //* Play Animation 
        transition.SetTrigger("Start");
        //* Wait
        yield return new WaitForSeconds(transitionTime);
        //* Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelByName(string sceneName)
    {
        //* 3 Steps 
        //* Play Animation 
        transition.SetTrigger("Start");
        //* Wait
        yield return new WaitForSeconds(transitionTime);
        //* Load Scene
        SceneManager.LoadScene(sceneName);
    }

}
