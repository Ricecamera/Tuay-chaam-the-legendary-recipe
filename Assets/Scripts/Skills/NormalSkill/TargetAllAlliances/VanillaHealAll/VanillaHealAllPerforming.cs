using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaHealAllPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        foreach (PakRender e in target)
        {
            e.gainHealing(8);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
