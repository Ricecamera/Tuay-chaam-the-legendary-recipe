using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffAtkOne : Skill
{
    //fields
    private string actionType = "TargetOneAlliance";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaBuffAtkOne(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        BuffAtkOneAlliance += ActionVanillaBuffAtkOne;
    }

    //delegates
    public Action<Entity, int> BuffAtkOneAlliance;
    //action
    private void ActionVanillaBuffAtkOne(Entity target, int value){
        target.Atk+=value;
        return;
    }
}
