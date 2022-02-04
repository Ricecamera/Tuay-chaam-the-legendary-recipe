using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffDefAll : Skill
{
    //fields
    private string actionType = "TargetAllAlliances";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaBuffDefAll(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        BuffDefAllAlliance += ActionVanillaBuffDefAll;
    }

    //delegates
    public Action<Entity[], int> BuffDefAllAlliance;
    //action
    public void ActionVanillaBuffDefAll(Entity[] target, int buffValue){
        foreach (Entity e in target){
            e.Def+=buffValue;
        }
        return;
    }
}
