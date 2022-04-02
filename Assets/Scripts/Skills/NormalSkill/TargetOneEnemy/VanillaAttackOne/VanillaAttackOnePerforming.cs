using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaAttackOnePerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        target[0].takeDamage(self, 1f);
        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOne", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
