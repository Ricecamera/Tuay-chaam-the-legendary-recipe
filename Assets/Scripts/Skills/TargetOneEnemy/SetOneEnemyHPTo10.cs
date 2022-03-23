using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetOneEnemyHPTo10 : Skill
{
    //constructor
    public SetOneEnemyHPTo10(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneEnemy")
    {
        SetOneEnemyHealthTo10 += ActionSetOneEnemyHPTo10;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Set one enemy health to 10";
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> SetOneEnemyHealthTo10;

    //action
    public void ActionSetOneEnemyHPTo10(List<PakRender> target, PakRender self)
    {
        if(target[0].healthSystem.CurrentHp <280) target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp-10);
        else target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp/2);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOneHard", soundBank[0].GetComponent<BattleSound>().clips);
        
        return;
    }
}
