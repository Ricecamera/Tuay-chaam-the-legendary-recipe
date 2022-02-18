using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image guage;

    private void OnEnable() {
        guage.fillAmount = 0;
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SetFill(float newValue)
    {
        if (newValue < 0)
            guage.fillAmount = 0;
        else if (newValue > 1)
            guage.fillAmount = 1;
        else
            guage.fillAmount = newValue;
    }

    public float getFill(){
        return guage.fillAmount;
    }

    public void Reset() {
        guage.fillAmount = 1;
    }
} 
