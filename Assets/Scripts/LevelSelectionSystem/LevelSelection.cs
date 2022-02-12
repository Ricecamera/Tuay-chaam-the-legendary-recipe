using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;

    public static int Level; // level of this obj.

    [SerializeField]
    public int levelname;


    public void PressSelection(string level)
    {
        if (unlocked)
        {
            Level = levelname;
            SceneManager.LoadScene(level);
        }
    }
}
