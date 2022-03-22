using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaHealOnePerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        int healValue = self.healthSystem.MaxHp / 4;
        target[0].healthSystem.Heal(healValue);
        return; 
    }
}
