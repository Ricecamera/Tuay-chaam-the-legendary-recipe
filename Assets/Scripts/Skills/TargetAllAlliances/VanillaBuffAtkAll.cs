using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffAtkAll : Skill
{
    //fields
    // private string actionType = "TargetAllAlliances";
    //getter
    // public string ActionType {
    //     get {return this.actionType;}
    // }
    //constructor
    public VanillaBuffAtkAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
    {
        BuffAtkAllAlliance += ActionVanillaBuffAtkAll;
        this.icon = icon;
    }

    //delegates
    public Action<PakRender[], PakRender> BuffAtkAllAlliance;
    //action
    public void ActionVanillaBuffAtkAll(PakRender[] target, PakRender self)
    {
        int buffValue = (self.pak.Atk) / 4;
        foreach (PakRender e in target)
        {
            e.pak.Atk += buffValue;
        }
        return;
    }
}
