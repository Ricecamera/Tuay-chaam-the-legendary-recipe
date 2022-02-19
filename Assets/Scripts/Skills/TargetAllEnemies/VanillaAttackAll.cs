using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackAll : Skill
{
    //fields
    private string actionType = "TargetAllEnemies";
    //getter
    public string ActionType
    {
        get { return this.actionType; }
    }
    //constructor
    public VanillaAttackAll(string skillId, string skillName, string description, int cooldown) : base(skillId, skillName, description, cooldown)
    {
        AttackAllEnemy += ActionVanillaAttackAll;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Attack all enemies at once.";
        this.cooldown = 0;
        /////////
    }

    //delegates
    public Action<PakRender[], PakRender> AttackAllEnemy;
    //action
    public void ActionVanillaAttackAll(PakRender[] target, PakRender self)
    {
        int damage;
        int atkValue = self.pak.Atk;
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            damage = (int)(atkValue * (float)(100 / (100 + e.pak.Def)));

            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
        }
        return;
    }
}