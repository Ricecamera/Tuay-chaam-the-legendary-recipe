using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaGainSPAll : Skill
{
    //fields
    private string actionType = "TargetAllAlliances";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaGainSPAll(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        GainSPAllAlliance += ActionVanillaGainSPAll;
    }

    //delegates
    public Action<PakRender[], PakRender> GainSPAllAlliance;
    //action
    public void ActionVanillaGainSPAll(PakRender[] target, PakRender self){
        int spValue = 10;
        foreach (PakRender e in target){
            e.pak.Sp+=spValue;
        }
        return;
    }
}
