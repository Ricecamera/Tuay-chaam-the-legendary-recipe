using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    public Animator transition;
    // Update is called once per frame

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SceneLoader");
                go.AddComponent<SceneLoader>();
                go.GetComponent<SceneLoader>().transition = go.AddComponent<Animator>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
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

    public void LoadSceneByIndex(int index)
    {
        StartCoroutine(LoadLevelByIndex(index));
    }

    public void LoadSceneByName(string name)
    {
        StartCoroutine(LoadLevelByName(name));
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
