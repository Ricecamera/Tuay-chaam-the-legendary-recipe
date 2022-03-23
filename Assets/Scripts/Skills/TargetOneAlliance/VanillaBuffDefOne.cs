using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VanillaBuffDefOne : Skill
{
    //constructor
    public VanillaBuffDefOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
    {
        BuffDefOneAlliance += ActionVanillaBuffDefOne;
        this.icon = icon;
    }

    //delegates
    public Action<List<PakRender>, PakRender> BuffDefOneAlliance;
    //action
    private void ActionVanillaBuffDefOne(List<PakRender> target, PakRender self)
    {
        target[0].currentDef += self.currentDef / 4;
        target[0].defBuffVfx.Play();

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
        
        return;
    }
}
