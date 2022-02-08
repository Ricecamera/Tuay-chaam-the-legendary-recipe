using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    private int currentHp;
    private int maxHp;
    public HealthBar healthBar;

    public int CurrentHp {
        get {
            return currentHp;
        }
        set {
            if (0 <= value && value <= maxHp)
                currentHp = value;
        }
    }

    public int MaxHp {
        get {
            return maxHp;
        }
        set {
            if (0 <= value && value <= maxHp)
                maxHp = value;
        }
    }

     public void Initialize(int maxHp) {
        Debug.Log("Health system work");
        this.maxHp = maxHp;
        this.currentHp = maxHp;
        healthBar.Reset();
    }

    public void TakeDamage(int damage , string name)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        float fill = currentHp / maxHp;
        healthBar.SetFill(fill);
        Debug.Log(name+":"+currentHp);

    }

    public void Heal(int healAmount,string name)
    {
        currentHp += healAmount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        float fill = currentHp / maxHp;
        healthBar.SetFill(fill);
        Debug.Log(name + ":" + currentHp);
    } 

    
    public void HideHpBar()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void ShowHpBar()
    {
        healthBar.gameObject.SetActive(true);
    }

}
