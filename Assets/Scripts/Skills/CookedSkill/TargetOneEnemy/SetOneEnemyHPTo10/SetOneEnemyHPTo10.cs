using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOneEnemyHPTo10 : Skill
{
    public SetOneEnemyHPTo10(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneEnemy", new SetOneEnemyHPTo10Performing(), "attackOneEnemy")
    {
        this.icon = icon;
    }
    
}
