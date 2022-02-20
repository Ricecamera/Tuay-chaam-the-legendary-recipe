using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaGainSPOne : Skill
{
    //fields
    // private string actionType = "TargetOneAlliance";
    // //getter
    // public string ActionType {
    //     get {return this.actionType;}

    //constructor
    public VanillaGainSPOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
    {
        GainSPOneAlliance += ActionVanillaGainSPOne;
        this.icon = icon;
    }
    //delegates
    public Action<PakRender, PakRender> GainSPOneAlliance;
    //action
    private void ActionVanillaGainSPOne(PakRender target, PakRender self)
    {
        target.pak.Sp += 30;
        return;
    }
}

