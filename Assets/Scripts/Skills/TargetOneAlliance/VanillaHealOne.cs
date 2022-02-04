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
    public Action<Entity, int> HealOneAlliance;
    //action
    private void ActionVanillaHealOne(Entity target, int value){
        target.Hp+=value;
        return;
    }
}
