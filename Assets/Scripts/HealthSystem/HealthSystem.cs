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
        Debug.Log(gameObject.name + " max hp is " + maxHp);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        float fill = currentHp / (float) maxHp;
        healthBar.SetFill(fill);


    }

    public void Heal(int healAmount)
    {
        currentHp += healAmount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        float fill = currentHp / (float) maxHp;
        healthBar.SetFill(fill);
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
