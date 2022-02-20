using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffAtkOne : Skill
{
    //constructor
    public VanillaBuffAtkOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
    {
        BuffAtkOneAlliance += ActionVanillaBuffAtkOne;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Target one alliance and buff its strength for the duration of this battle.";
        this.cooldown = 1;
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> BuffAtkOneAlliance;
    //action
    private void ActionVanillaBuffAtkOne(List<PakRender> target, PakRender self)
    { //should pass lv and stat of char and calculate value in this function.
        target[0].pak.Atk += self.pak.Atk / 4;
        return;
    }
}
