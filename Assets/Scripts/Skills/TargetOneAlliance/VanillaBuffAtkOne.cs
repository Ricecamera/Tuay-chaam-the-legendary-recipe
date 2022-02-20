using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffAtkOne : Skill
{
    //fields
    // private string actionType = "TargetOneAlliance";
    // //getter
    // public string ActionType
    // {
    //     get { return this.actionType; }
    // }
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
    public Action<PakRender, PakRender> BuffAtkOneAlliance;
    //action
    private void ActionVanillaBuffAtkOne(PakRender target, PakRender self)
    { //should pass lv and stat of char and calculate value in this function.
        target.pak.Atk += self.pak.Atk / 4;
        return;
    }
}
