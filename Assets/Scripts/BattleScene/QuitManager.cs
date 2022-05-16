using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Battle Tutorial")
        {
            gameObject.SetActive(false);
        }
    }
    public void quit()
    {
        SceneLoader.Instance.LoadSceneByName("opened-map");
    }
}
