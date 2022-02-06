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
    public Action<PakRender[], PakRender> BuffAtkAllAlliance;
    //action
    public void ActionVanillaBuffAtkAll(PakRender[] target, PakRender self){
        int buffValue = (self.pak.Atk)/4;
        foreach (PakRender e in target){
            e.pak.Atk+=buffValue;
        }
        return;
    }
}
