using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{

    private int currentHp;
    private int maxHp;
    public HealthBar hp;

    // public void initHealth(int maxHp){
    //     this.maxHp = maxHp;
    //     this.currentHp = maxHp;
    //     hp.SetMaxHealth(maxHp);
    // }
        

     public HealthSystem(int maxHp,HealthBar hp) {
        Debug.Log("Health system work");
        this.maxHp = maxHp;
        currentHp = maxHp;
        this.hp = hp;
        hp.SetMaxHealth(maxHp);
    }

    public int GetHealth()
    {
        return currentHp;
    }

    public void TakeDamage(int damage , string name)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
        hp.SetHealth(currentHp);

        Debug.Log(name+":"+currentHp);

    }

    public void Heal(int healAmount,string name)
    {
        currentHp += healAmount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hp.SetHealth(currentHp);
        Debug.Log(name + ":" + currentHp);
    } 


}
