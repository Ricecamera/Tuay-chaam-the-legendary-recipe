using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealOne : Skill
{
    //constructor
    public VanillaHealOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
    {
        HealOneAlliance += ActionVanillaHealOne;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Target one alliance and heal it.";
        this.cooldown = 1;
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> HealOneAlliance;
    //action
    private void ActionVanillaHealOne(List<PakRender> target, PakRender self)
    {
        int healValue = self.healthSystem.MaxHp / 4;

        target[0].healthSystem.Heal(healValue);
        return;
    }
}
