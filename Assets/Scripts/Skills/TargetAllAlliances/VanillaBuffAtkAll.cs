using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffAtkAll : Skill
{
    //fields
    private string actionType = "TargetAllAlliances";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaBuffAtkAll(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        BuffAtkAllAlliance += ActionVanillaBuffAtkAll;
    }

    //delegates
    public Action<Entity[], int> BuffAtkAllAlliance;
    //action
    public void ActionVanillaBuffAtkAll(Entity[] target, int buffValue){
        foreach (Entity e in target){
            e.Atk+=buffValue;
        }
        return;
    }
}
