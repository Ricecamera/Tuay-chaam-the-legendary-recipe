using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTargetEnemies : Skill
{
    public AllTargetEnemies(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){

    }

    public void VanillaAttackAll(PakRender[] target, int atkValue) {
        int damage;
        foreach (var e in target){
            if(atkValue - e.pak.Def <=0) damage=0;
            else damage = atkValue - e.pak.Def;
            e.healthSystem.TakeDamage(damage);
        }       
        return;
    }
}
