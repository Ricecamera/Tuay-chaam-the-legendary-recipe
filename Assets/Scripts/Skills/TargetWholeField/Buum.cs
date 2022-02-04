using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Buum : Skill
{
    //fields
    private string actionType = "TargetWholeField";
    //getter
    public string ActionType {
        get {return this.actionType;}
    }
    //constructor
    public Buum(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){
        AttackWholeField += ActionBuum;
    }

    //delegates
    public Action<Entity[], Entity> AttackWholeField;

    //action
    private void ActionBuum(Entity[] target, Entity self){
        int damage = (int)(self.Hp*0.6);
        foreach (Entity e in target){
            if(damage - e.Def <=0) damage=0;
            e.Hp-=damage;
            if(e.Hp<=0) e.Hp=0;
        }
        self.Hp=0;
        return;
    }
}
