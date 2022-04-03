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
<<<<<<< HEAD
            }
            if (SceneManager.GetActiveScene().buildIndex == 9) //* Character Select
=======

                // Test

                Button newGameButton = GameObject.Find("New Game").GetComponent<Button>();
                Button loadGameButton = GameObject.Find("Load Game").GetComponent<Button>();
                newGameButton.onClick.AddListener(LoadNextScene);
                loadGameButton.onClick.AddListener(LoadNextScene);

                //
            }
            if (SceneManager.GetActiveScene().buildIndex == 3) //* Character Select
>>>>>>> parent of 95d7c80 (Merge branch 'New-Scene-Loader' into Database-+-Save/Load-V1)
            {
                Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
                Button backButton = GameObject.Find("BackButton").GetComponent<Button>();
                Button helpButton = GameObject.Find("HelpButton").GetComponent<Button>();
<<<<<<< HEAD
                startButton.onClick.AddListener(LoadNextScene);
                backButton.onClick.AddListener(LoadPrevScene);
                helpButton.onClick.AddListener(Help);
=======
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
                });
                backButton.onClick.AddListener(LoadPrevScene);
                helpButton.onClick.AddListener(Help);

>>>>>>> parent of 95d7c80 (Merge branch 'New-Scene-Loader' into Database-+-Save/Load-V1)
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
<<<<<<< HEAD
        Debug.Log("click");
=======
        //Debug.Log("click");
>>>>>>> parent of 95d7c80 (Merge branch 'New-Scene-Loader' into Database-+-Save/Load-V1)
        StartCoroutine(LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPrevScene()
    {
<<<<<<< HEAD
        Debug.Log("back");
=======
        //Debug.Log("back");
>>>>>>> parent of 95d7c80 (Merge branch 'New-Scene-Loader' into Database-+-Save/Load-V1)
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
