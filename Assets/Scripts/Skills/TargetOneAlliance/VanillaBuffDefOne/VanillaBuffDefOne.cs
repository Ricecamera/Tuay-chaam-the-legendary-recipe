using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffDefOne : Skill
{
    //constructor
    public VanillaBuffDefOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance", new VanillaBuffDefOnePerforming(), "buffOneAlliance")
    {
        // BuffDefOneAlliance += ActionVanillaBuffDefOne;
        this.icon = icon;
    }

    // //delegates
    // public Action<List<PakRender>, PakRender> BuffDefOneAlliance;
    // //action
    // private void ActionVanillaBuffDefOne(List<PakRender> target, PakRender self)
    // {
    //     target[0].currentDef += self.currentDef / 4;
    //     target[0].defBuffVfx.Play();
    //     return;
    // }
}
