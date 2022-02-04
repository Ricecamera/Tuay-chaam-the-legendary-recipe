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
    public Action<Entity[], int> AttackAllEnemy;
    //action
    public void ActionVanillaAttackAll(Entity[] target, int atkValue) {
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
