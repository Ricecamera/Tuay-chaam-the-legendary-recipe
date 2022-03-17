using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour {
    
    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SceneLoader.Instance.LoadNextScene();
        }
    }   
}

