using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackAll : Skill
{
    //constructor
    public VanillaAttackAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllEnemies")
    {
        AttackAllEnemy += ActionVanillaAttackAll;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Attack all enemies at once.";
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> AttackAllEnemy;
    //action
    public void ActionVanillaAttackAll(List<PakRender> target, PakRender self)
    {
        int damage;
        int atkValue = self.pak.Atk;
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            damage = (int)(atkValue * (float)(100 / (100 + e.pak.Def)));
            if (damage <= 0)
            {
                damage = 20;
            }
            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }
        return;
    }
}