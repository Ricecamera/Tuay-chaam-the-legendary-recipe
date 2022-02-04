using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWholeField : Skill
{
    public TargetWholeField(string skillId, string skillName, string description, int cooldown):base(skillId, skillName, description, cooldown){

    }
    //all skills
    public void Buum(Entity[] target, Entity self){
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
