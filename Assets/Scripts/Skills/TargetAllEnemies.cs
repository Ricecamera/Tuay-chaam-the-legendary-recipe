using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTargetEnemies : Skill
{
    public AllTargetEnemies(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){

    }

    public void VanillaAttackAll(Entity[] target, int atkValue) {
        int damage;
        foreach (Entity e in target){
            if(atkValue - e.Def <=0) damage=0;
            else damage = atkValue - e.Def;
            e.Hp-=damage;
            if(e.Hp<=0) e.Hp=0;
        }       
        return;
    }
}
