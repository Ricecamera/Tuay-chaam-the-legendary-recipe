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
    public Action<PakRender[], PakRender> BuffDefAllAlliance;
    //action
    public void ActionVanillaBuffDefAll(PakRender[] target, PakRender self){
        int buffValue = (self.pak.Def)/4;
        foreach (PakRender e in target){
            e.pak.Def+=buffValue;
        }
        return;
    }
}
