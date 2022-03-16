using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour
{
    public bool isInVictoryScene;
    // Start is called before the first frame update
    public void pressMap(string level)
    {
        if (LevelManager.instance.winTime == 2 && isInVictoryScene) {
            SceneManager.LoadScene("CutScene_4");
        }
        else if (LevelManager.instance.winTime == 1 && isInVictoryScene) {
            SceneManager.LoadScene("CutScene_3");
        }
        else
            SceneManager.LoadScene(level);
    }
}

