using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaHealAllPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
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
