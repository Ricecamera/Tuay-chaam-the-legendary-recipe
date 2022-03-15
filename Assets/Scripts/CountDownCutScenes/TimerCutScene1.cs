using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCutScene1 : MonoBehaviour
{
    private string levelToLoad="Default_level_1-1";
    private float timer = 43f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0){
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
