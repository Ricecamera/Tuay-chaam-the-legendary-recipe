using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider ;

    public void SetMaxHealth(int health)
    {
        Debug.Log("SetMaxHealth HealthBar work");
        slider.maxValue= health;
        slider.value = health;
        Debug.Log("Slider value:"+slider.value);
        Debug.Log("Slider Maxvalue:"+slider.maxValue);
        
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public float getHealth(){
        return slider.value;
    }

} 
