using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionPP : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;

    public static int Level; // level of this obj.

    [SerializeField]
    public int levelname;

    [SerializeField]
    private Image padLock;

    [SerializeField]
    private Animator transition;


    void Start()
    {

        padLock.enabled = true;
        unlocked = LevelManager.instance.unlockStatus[levelname - 1];

        if (unlocked)
        {
            padLock.enabled = false;
            padLock.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Update()
    {
        if (unlocked)
        {
            transition.SetTrigger("Unlock");
            padLock.enabled = false;
            padLock.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            padLock.enabled = true;
            padLock.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void PressSelection(string level)
    {
        if (unlocked)
        {
            Level = levelname;
            LevelManager.instance.thislevel = levelname;
            SceneManager.LoadScene(level);
        }
    }

    public bool getUnlocked()
    {
        return unlocked;
    }



}
