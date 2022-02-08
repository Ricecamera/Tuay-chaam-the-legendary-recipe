using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealOne : Skill
{
    //fields
    private string actionType = "TargetOneAlliance";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaHealOne(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        HealOneAlliance += ActionVanillaHealOne;
    }

    //delegates
    public Action<PakRender, PakRender> HealOneAlliance;
    //action
    private void ActionVanillaHealOne(PakRender target, PakRender self){
        int healValue = self.healthSystem.MaxHp/4;

        target.healthSystem.Heal(healValue);
        return;
    }
}
