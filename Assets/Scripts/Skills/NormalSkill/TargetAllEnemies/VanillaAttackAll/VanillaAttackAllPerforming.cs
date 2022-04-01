using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaAttackAllPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        foreach (PakRender e in target)
        {
            e.takeDamage(self, 1.6f);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitAll", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
