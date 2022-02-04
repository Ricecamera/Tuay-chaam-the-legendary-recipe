using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaGainSPOne : Skill
{
    //fields
    private string actionType = "TargetOneAlliance";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaGainSPOne(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        GainSPOneAlliance += ActionVanillaGainSPOne;
    }

    //delegates
    public Action<Pak, int> GainSPOneAlliance;
    //action
    private void ActionVanillaGainSPOne(Pak target, int value){
        target.Sp+=value;
        return;
    }
}
