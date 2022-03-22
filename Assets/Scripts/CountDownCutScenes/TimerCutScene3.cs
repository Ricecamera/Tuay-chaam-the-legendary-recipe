using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCutScene3 : MonoBehaviour
{
    private string levelToLoad="ModeSelectionForTutorial";
    private float timer = 1f; //25f;
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
