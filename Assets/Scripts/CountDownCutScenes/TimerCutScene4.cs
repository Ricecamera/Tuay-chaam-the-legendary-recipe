using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCutScene4 : MonoBehaviour
{
    private string levelToLoad = "opened-map";
    private float timer = 59f; //59f
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
