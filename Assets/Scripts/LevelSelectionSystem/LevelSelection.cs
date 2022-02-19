using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;

    public static int Level; // level of this obj.

    [SerializeField]
    public int levelname;

    [SerializeField]
    private Image padLock;


    void Start()
    {
        padLock.enabled = true;
        if (levelname == 1)
        {
            unlocked = true;
        }
    }

    void Update()
    {
        if (unlocked)
        {
            padLock.enabled = false;
        }
        else
        {
            padLock.enabled = true;
        }
    }

    public void PressSelection(string level)
    {
        if (unlocked)
        {
            Level = levelname;
            SceneManager.LoadScene(level);
        }
    }
}
