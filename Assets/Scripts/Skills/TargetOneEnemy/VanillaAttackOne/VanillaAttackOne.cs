using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackOne : Skill
{
    //constructor
    public VanillaAttackOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneEnemy", new VanillaAttackOnePerforming(), "attackOneEnemy")
    {
        // AttackOneEnemy += ActionVanillaAttackOne;
        // //Yod Add this for use temp skill desc and cooldown //// 
        // this.description = "Target one opponent to perform an attack";
        this.icon = icon;
        /////////

    }

    // //delegates
    // public Action<List<PakRender>, PakRender> AttackOneEnemy;
    // //action
    // private void ActionVanillaAttackOne(List<PakRender> target, PakRender self)
    // {
    //     int damage;
    //     int atkValue = self.currentAtk;
    //     damage = (int)(atkValue * (decimal)(100f / (100f + target[0].currentDef)));

    //     //target.pak.Hp-=damage;                  //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
    //     //if(target.pak.Hp<=0) target.pak.Hp=0;   //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
    //     target[0].healthSystem.TakeDamage(damage);
    //     target[0].switchMat();
    //     return;
    // }
}