using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
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

    [SerializeField]
    private GameObject label;


    void Start()
    {
        label.GetComponent<TextMeshProUGUI>().text = "1-" + levelname.ToString();
        unlocked = LevelManager.instance.unlockStatus[levelname - 1];
        if (unlocked)
        {
            padLock.gameObject.SetActive(false);
            //padLock.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            padLock.enabled = true;
            padLock.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    void Update()
    {
        // if(unlocked){
        //     if (!LevelManager.instance.playAniAlreadyMap[levelname - 1])
        //     {
        //         Debug.Log("Get into playAnimation clause ");
        //         transition.SetTrigger("Unlock");
        //         LevelManager.instance.playAniAlreadyMap[levelname - 1] = true;
        //     }
        // }
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