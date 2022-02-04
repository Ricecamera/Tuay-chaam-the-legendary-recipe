using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffDefOne : Skill
{
    //fields
    private string actionType = "TargetOneAlliance";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaBuffDefOne(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        BuffDefOneAlliance += ActionVanillaBuffDefOne;
    }

    //delegates
    public Action<Entity, int> BuffDefOneAlliance;
    //action
    private void ActionVanillaBuffDefOne(Entity target, int value){
        target.Def+=value;
        return;
    }
}
