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
    public Action<PakRender[], PakRender> AttackWholeField;

    //action
    private void ActionBuum(PakRender[] target, PakRender self){ //target can be the list of all Pakrender in the fighting scene.
        int damage;
        foreach (PakRender e in target){
            damage = (int)(self.pak.Hp*0.6);
            if(damage - e.pak.Def <=0) damage=0;

            e.pak.Hp-=damage;                //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            if(e.pak.Hp<=0) e.pak.Hp=0;      //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage, e.pak.Hp.ToString());
        }
        self.pak.Hp=0;  //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        self.healthSystem.TakeDamage(self.healthSystem.GetHealth(), "0");
        // use damage funcction from health system instead.
        return;
    }
}