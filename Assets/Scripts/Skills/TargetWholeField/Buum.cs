using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Buum : Skill
{
    //fields
    // private string actionType = "TargetWholeField";
    // //getter
    // public string ActionType {
    //     get {return this.actionType;}
    // }
    //constructor
    public Buum(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetWholeField")
    {
        AttackWholeField += ActionBuum;
        this.icon = icon;
    }

    //delegates
    public Action<PakRender[], PakRender> AttackWholeField;

    //action
    private void ActionBuum(PakRender[] target, PakRender self)
    { //target can be the list of all Pakrender in the fighting scene.
        int damage = (int)(self.healthSystem.MaxHp * 0.4);
        foreach (PakRender e in target)
        {
            damage = (int)(self.healthSystem.CurrentHp * 0.6);
            if (damage - e.pak.Def <= 0) damage = 0;
            e.healthSystem.TakeDamage(damage);
        }
        self.healthSystem.TakeDamage(1000);
        return;
    }
}