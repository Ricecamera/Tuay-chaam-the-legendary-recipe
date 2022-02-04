using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTargetEnemy : Skill
{
    public OneTargetEnemy(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){

    }

    //all skills
    public void VanillaAttack(Entity target, int atkValue) {
        int damage;
        if(atkValue - target.Def <=0) damage=0;
        else damage = atkValue - target.Def;
        target.Hp-=damage;
        if(target.Hp<=0) target.Hp=0;
        return;
    }
}
