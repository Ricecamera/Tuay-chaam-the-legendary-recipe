using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackOne : Skill
{
    //fields
    private string actionType = "TargetOneEnemy";


    //getter
    public string ActionType
    {
        get { return this.actionType; }
    }
    //constructor
    public VanillaAttackOne(string skillId, string skillName, string description, int cooldown) : base(skillId, skillName, description, cooldown)
    {
        AttackOneEnemy += ActionVanillaAttackOne;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Target one opponent to perform an attack";
        this.cooldown = 0;
        /////////

    }

    //delegates
    public Action<PakRender, PakRender> AttackOneEnemy;
    //action
    private void ActionVanillaAttackOne(PakRender target, PakRender self)
    {
        int damage;
        int atkValue = self.pak.Atk;
        damage = (int)(atkValue * (decimal)(100f / (100f + target.pak.Def)));

        //target.pak.Hp-=damage;                  //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        //if(target.pak.Hp<=0) target.pak.Hp=0;   //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        Debug.Log("The attack is " + atkValue.ToString());
        Debug.Log("The def is " + target.pak.Def.ToString());
        Debug.Log("0ro is " + (100f + target.pak.Def));
        Debug.Log("1st is " + (decimal)(100f / (100f + target.pak.Def)));
        Debug.Log("2st is " + atkValue * (decimal)(100f / (100f + target.pak.Def)));
        Debug.Log("The damage is " + damage.ToString());
        target.healthSystem.TakeDamage(damage);
        return;
    }
}