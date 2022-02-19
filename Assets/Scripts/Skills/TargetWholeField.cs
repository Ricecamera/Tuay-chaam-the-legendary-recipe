using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWholeField : Skill
{
    public TargetWholeField(string skillId, string skillName, string description, int cooldown) : base(skillId, skillName, description, cooldown)
    {

    }
    //all skills
    public void Buum(PakRender[] target, PakRender self)
    {
        int damage = (int)(self.healthSystem.CurrentHp * 0.6);
        foreach (PakRender e in target)
        {
            if (damage - e.pak.Def <= 0) damage = 0;
            e.healthSystem.TakeDamage(damage);
        }
        self.healthSystem.CurrentHp = 0;
        return;
    }
}