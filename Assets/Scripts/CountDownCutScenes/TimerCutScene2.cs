using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCutScene2 : MonoBehaviour
{
    private string levelToLoad = "Battle1-2V2";
    private float timer = 73f; //73f
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
