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
    public Action<Pak[], int> GainSPAllAlliance;
    //action
    public void ActionVanillaGainSPAll(Pak[] target, int spValue){
        foreach (Pak e in target){
            e.Sp+=spValue;
        }
        return;
    }
}
