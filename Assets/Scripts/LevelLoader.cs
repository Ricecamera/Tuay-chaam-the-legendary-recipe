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
                playButton.onClick.AddListener(() => SceneManager.LoadScene("ModeSelection"));
                quitButton.onClick.AddListener(ExitGame);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3) //* Character Select
            {
                Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
                Button backButton = GameObject.Find("BackButton").GetComponent<Button>();
                // Button helpButton = GameObject.Find("HelpButton").GetComponent<Button>();
                startButton.onClick.AddListener(() =>
                {
                    if (CharacterSelecter.instance.GetCharacters().Count < 1 || CharacterSelecter.instance.GetChaam() == null)
                    {
                        CharacterSelecter.instance.ShowPopup();
                    }
                    else
                    {
                        LoadNextScene();
                    }
                }); ;
            }
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPrevScene()
    {
        StartCoroutine(LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadSpecificScene(string sceneName)
    {

        StartCoroutine(LoadLevelByName(sceneName));
    }

    public void StartGame()
    {
        StartCoroutine(LoadLevelByIndex(10));
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
