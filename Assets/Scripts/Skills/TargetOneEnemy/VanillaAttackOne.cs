using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackOne : Skill
{
    //fields
    private string actionType = "TargetOneEnemy";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaAttackOne(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        AttackOneEnemy += ActionVanillaAttackOne;
    }

    //delegates
    public Action<Entity, int> AttackOneEnemy;
    //action
    private void ActionVanillaAttackOne(Entity target, int atkValue) {
        int damage;
        if(atkValue - target.Def <=0) damage=0;
        else damage = atkValue - target.Def;
        target.Hp-=damage;
        if(target.Hp<=0) target.Hp=0;
        return;
    }
}
