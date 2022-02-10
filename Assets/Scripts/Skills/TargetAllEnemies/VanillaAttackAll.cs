using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackAll : Skill
{
    //fields
    private string actionType = "TargetAllEnemies";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public VanillaAttackAll(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        AttackAllEnemy += ActionVanillaAttackAll;
    }

    //delegates
    public Action<PakRender[], PakRender> AttackAllEnemy;
    //action
    public void ActionVanillaAttackAll(PakRender[] target, PakRender self) {
        int damage;
        int atkValue = self.pak.Atk;
        foreach (PakRender e in target){
            if(atkValue - e.pak.Def <=0) damage=0;
            else damage = atkValue - e.pak.Def;
            
            e.healthSystem.TakeDamage(damage);
        }       
        return;
    }
}