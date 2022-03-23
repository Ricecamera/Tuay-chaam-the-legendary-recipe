using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HalfEnemiesHealth : Skill
{
    //constructor
    public HalfEnemiesHealth(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllEnemies")
    {
        HalfAllEnemiesHealth += ActionHalfEnemiesHealth;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Half health of all enemies.";
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> HalfAllEnemiesHealth;

    //action
    public void ActionHalfEnemiesHealth(List<PakRender> target, PakRender self)
    {
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            int damage = (int) (e.healthSystem.CurrentHp/2);
            if (damage <= 0)
            {
                damage = 1;
            }
            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }
        return;
    }
}
