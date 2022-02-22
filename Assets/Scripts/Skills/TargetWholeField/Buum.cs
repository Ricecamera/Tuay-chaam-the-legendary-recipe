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
        int damage = (int)(self.healthSystem.MaxHp * 0.6);
        target.Remove(self);
        foreach (PakRender e in target)
        {
            if (damage - e.currentDef <= 0) damage = 0;
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }
        self.healthSystem.TakeDamage((int)(self.healthSystem.CurrentHp / 1.5));
        return;
    }
}