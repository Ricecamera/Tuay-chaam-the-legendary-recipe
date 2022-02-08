using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTargetAlliance : Skill
{
    public OneTargetAlliance(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){

    }
    //all skills
    public void VanillaHeal(Entity target, int value){
        target.MaxHp += value;
        return;
    }

    public void VanillaGainSP(Pak target, int value){
        target.Sp+=value;
        return;
    }

    public void VanillaBuffAtk(Entity target, int value){
        target.Atk+=value;
        return;
    }

    public void VanillaBuffDef(Entity target, int value){
        target.Def+=value;
        return;
    }
}
