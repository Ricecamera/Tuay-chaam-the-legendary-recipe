using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatScene : MonoBehaviour
{
    [SerializeField]
    private GameObject clickToContinue;
    private int opacity=0;
    private int textMode = 0; //0 = gose up, 1 = goes down
    TextMeshProUGUI theClickToContinue;
    void Start()
    {
        CharacterSelecter.instance.ResetCharacter();

        theClickToContinue = clickToContinue.GetComponent<TextMeshProUGUI>();
        Debug.Log(theClickToContinue);
        theClickToContinue.faceColor = new Color32(255,255,255,0);
        theClickToContinue.outlineColor = new Color32(0,0,0,0);
    }

    private bool updateTrigger = false;
    private int updateCount=0;

    private void Update() {
        updateCount++;
        if(updateCount>=1000){
            if(opacity==255) textMode = 1;
            else if(opacity==0) textMode = 0;

            theClickToContinue.faceColor = new Color32(255,255,255,(byte)opacity);
            theClickToContinue.outlineColor = new Color32(0,0,0,(byte)opacity);

            if(textMode==0 && updateTrigger){
                opacity++; updateTrigger=false;
            } 
            else if(textMode==0) updateTrigger=true;

            if(textMode==1 && updateTrigger){
                opacity--; updateTrigger=false;
            } 
            else if(textMode==1) updateTrigger=true;  
        }

        if(Input.GetMouseButtonDown(0) && updateCount>=1200){
            goToNextScene();
        }
    }

    private void goToNextScene(){
        SaveManager.instance.SetZeroDieCount();
        SaveManager.instance.Save();
        // if (LevelManager.instance.winTime == 2 && isInVictoryScene)
        // {
        //     SceneManager.LoadScene("CutScene_4");
        // }
        // else if (LevelManager.instance.winTime == 1 && isInVictoryScene)
        // {
        //     SceneManager.LoadScene("CutScene_3");
        // }
        // else
            SceneManager.LoadScene("opened-map");
    }
}
