using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealAll : Skill
{
    //fields
    private string actionType = "TargetAllAlliances";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaHealAll(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        HealAllAlliance += ActionVanillaHealAll;
    }

    //delegates
    public Action<PakRender[], PakRender> HealAllAlliance;
    //action
    public void ActionVanillaHealAll(PakRender[] target, PakRender self){
        int healValue = self.pak.Hp/4;
        foreach (PakRender e in target){
            e.pak.Hp+=healValue;    //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.Heal(healValue, e.pak.Hp.ToString());
        }
        return;
    }
}
