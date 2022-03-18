using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour {
    // Update is called once per 
    private void Start() {
        if(SceneLoader.Instance == null){
            SceneLoader.Instance.Awake();
        }
    }

    private void Update() {
        
    }   
}

