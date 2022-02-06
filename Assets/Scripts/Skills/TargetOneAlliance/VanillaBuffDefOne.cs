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
    public Action<PakRender, PakRender> BuffDefOneAlliance;
    //action
    private void ActionVanillaBuffDefOne(PakRender target, PakRender self){
        target.pak.Def+=self.pak.Def/4;
        return;
    }
}
