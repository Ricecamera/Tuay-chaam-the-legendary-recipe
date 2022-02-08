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
    public Action<PakRender, PakRender> AttackOneEnemy;
    //action
    private void ActionVanillaAttackOne(PakRender target, PakRender self) {
        int damage;
        int atkValue = self.pak.Atk;
        if(atkValue - target.pak.Def <=0) damage=0;
        else damage = atkValue - target.pak.Def;
        
        //target.pak.Hp-=damage;                  //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        //if(target.pak.Hp<=0) target.pak.Hp=0;   //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        target.healthSystem.TakeDamage(damage, target.pak.Hp.ToString());
        return;
    }
}