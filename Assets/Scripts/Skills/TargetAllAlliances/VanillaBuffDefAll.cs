using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffDefAll : Skill
{
    //constructor
    public VanillaBuffDefAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
    {
        BuffDefAllAlliance += ActionVanillaBuffDefAll;
        this.icon = icon;
    }

    //delegates
    public Action<List<PakRender>, PakRender> BuffDefAllAlliance;
    //action
    public void ActionVanillaBuffDefAll(List<PakRender> target, PakRender self)
    {
        int buffValue = (self.currentDef) / 6;
        foreach (PakRender e in target)
        {
            e.currentDef += buffValue;
            e.defBuffVfx.Play();
        }
        return;
    }
}
