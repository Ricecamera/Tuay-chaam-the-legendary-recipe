using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaHealAll : Skill
{
    //constructor
    public VanillaHealAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
    {
        HealAllAlliance += ActionVanillaHealAll;
        //Yod Add this for use temp skill desc and cooldown //// 
        this.description = "Heal all alliances at once.";
        this.icon = icon;
        /////////
    }

    //delegates
    public Action<List<PakRender>, PakRender> HealAllAlliance;
    //action
    public void ActionVanillaHealAll(List<PakRender> target, PakRender self)
    {
        int healValue = self.healthSystem.MaxHp / 8;
        foreach (PakRender e in target)
        {
            e.healthSystem.CurrentHp += healValue;    //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.Heal(healValue);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
        
        return;
    }
}
