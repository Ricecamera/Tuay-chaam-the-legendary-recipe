using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTargetEnemy : Skill
{
    public OneTargetEnemy(string skillId, string skillName, string description, int cooldown) : base(skillId, skillName, description, cooldown)
    {

    }

    //all skills
    public void VanillaAttack(PakRender target, int atkValue)
    {
        int damage;
        if (atkValue - target.pak.Def <= 0) damage = 0;
        else damage = atkValue - target.pak.Def;
        target.healthSystem.TakeDamage(damage);
        return;
    }
}