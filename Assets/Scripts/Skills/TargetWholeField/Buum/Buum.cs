using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Buum : Skill
{
    //constructor
    public Buum(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetWholeField", new BuumPerforming(), "attackWholeField")
    {
        this.icon = icon;
    }

    //delegates
    // public Action<List<PakRender>, PakRender> GoBuum;

    //action
    // private void ActionBuum(List<PakRender> target, PakRender self)
    
}