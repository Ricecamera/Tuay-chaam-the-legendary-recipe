using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealOne : Skill
{
    //fields
    //private string actionType = "TargetOneAlliance";
    //getter
    // public string ActionType
    // {
    //     get { return this.actionType; }
    // }
    //constructor
    public VanillaHealOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
    {
        HealOneAlliance += ActionVanillaHealOne;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Target one alliance and heal it.";
        this.cooldown = 1;
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<PakRender, PakRender> HealOneAlliance;
    //action
    private void ActionVanillaHealOne(PakRender target, PakRender self)
    {
        int healValue = self.healthSystem.MaxHp / 4;

        target.healthSystem.Heal(healValue);
        return;
    }
}
