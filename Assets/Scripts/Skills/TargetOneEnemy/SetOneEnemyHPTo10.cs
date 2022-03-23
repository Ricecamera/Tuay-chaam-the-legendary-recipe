using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetOneEnemyHPTo10 : Skill
{
    //constructor
    public SetOneEnemyHPTo10(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneEnemies")
    {
        SetOneEnemyHealthTo10 += SetOneEnemyHealthTo10;
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
            target[0].healthSystem.CurrentHp=10;
        return;
    }
}
