using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfEnemiesHealth : Skill
{
    public HalfEnemiesHealth(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllEnemies", new HalfEnemiesHealthPerforming(), "attackAllEnemies")
    {
        // BuffAtkAllAlliance += ActionVanillaBuffAtkAll;
        this.icon = icon;
    }
}
