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
    public Action<Entity[], int> HealAllAlliance;
    //action
    public void ActionVanillaHealAll(Entity[] target, int healValue){
        foreach (Entity e in target){
            e.Hp+=healValue;
        }
        return;
    }
}
