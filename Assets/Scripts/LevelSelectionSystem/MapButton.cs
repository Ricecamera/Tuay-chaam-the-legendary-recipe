using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void pressMap(string level)
    {
        SceneManager.LoadScene(level);
    }

}
