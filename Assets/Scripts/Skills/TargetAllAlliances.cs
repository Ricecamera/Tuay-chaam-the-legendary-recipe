using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTargetAlliances : Skill
{
    public AllTargetAlliances(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
    {

    }

    public void VanillaHeal(PakRender[] target, int healValue)
    {
        foreach (var e in target)
        {
            e.healthSystem.Heal(healValue);
        }
        return;
    }

    public void VanillaGainSP(Pak[] target, int spValue)
    {
        foreach (Pak e in target)
        {
            e.Sp += spValue;
        }
        return;
    }

    public void VanillaBuffAtk(Entity[] target, int buffValue)
    {
        foreach (Entity e in target)
        {
            e.Atk += buffValue;
        }
        return;
    }

    public void VanillaBuffDef(Entity[] target, int buffValue)
    {
        foreach (Entity e in target)
        {
            e.Def += buffValue;
        }
        return;
    }
}