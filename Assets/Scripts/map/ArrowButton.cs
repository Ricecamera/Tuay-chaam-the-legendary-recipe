using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int mode; //1 is up, 0 is down
    public Button button;

    // private Image theButton;
    private GameObject chaam;

    void Start() {
        // theButton= (mode==0) ?  GameObject.Find("CameraDownฺButton").GetComponent<Image>() : GameObject.Find("CameraUpButton").GetComponent<Image>();
        chaam = GameObject.Find("broken-Chaam-EMOT_0");
    }

    void Update() {
        if (mode==1){
            if(chaam.GetComponent<Transform>().position.y >= 12.0){
                // TChaamController.instance.vertical=0;
                button.interactable=false;
                // Debug.Log("Up Disable");
            }else{
                button.interactable=true;
                // Debug.Log("Up Enable");
            }
        }else{
            if(chaam.GetComponent<Transform>().position.y <= -5.2){
                // TChaamController.instance.vertical=0;
                button.interactable=false;
                // Debug.Log("Down Disable");
            }else{
                button.interactable=true;
                // Debug.Log("Down Enable");
            }
        }
        
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Hold Down");
        TChaamController.instance.vertical = (mode==1) ? 1:-1;
    }

    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Hold Up");
        TChaamController.instance.vertical=0;
    }
}
