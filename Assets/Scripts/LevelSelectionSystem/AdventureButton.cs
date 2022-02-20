using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureButton : MonoBehaviour
{
    public void pressAdventure(string level)
    {
        SceneManager.LoadScene(level);
    }
}
