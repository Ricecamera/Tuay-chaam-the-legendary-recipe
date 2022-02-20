using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Buum : Skill
{
    //constructor
    public Buum(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetWholeField")
    {
        AttackWholeField += ActionBuum;
        this.icon = icon;
    }

    //delegates
    public Action<List<PakRender>, PakRender> AttackWholeField;

    //action
    private void ActionBuum(List<PakRender> target, PakRender self)
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