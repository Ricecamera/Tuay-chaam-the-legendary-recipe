using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealAll : Skill
{
    //fields
    // private string actionType = "TargetAllAlliances";
    // //getter
    // public string ActionType
    // {
    //     get { return this.actionType; }
    // }
    //constructor
    public VanillaHealAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
    {
        HealAllAlliance += ActionVanillaHealAll;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Heal all alliances at once.";
        this.cooldown = 2;
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<PakRender[], PakRender> HealAllAlliance;
    //action
    public void ActionVanillaHealAll(PakRender[] target, PakRender self)
    {
        int healValue = self.healthSystem.MaxHp / 4;
        foreach (PakRender e in target)
        {
            e.healthSystem.CurrentHp += healValue;    //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.Heal(healValue);
        }
        return;
    }
}
