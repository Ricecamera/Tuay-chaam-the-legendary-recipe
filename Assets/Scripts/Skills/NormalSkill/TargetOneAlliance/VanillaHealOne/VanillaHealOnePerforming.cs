using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaHealOnePerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {

        target[0].gainHealing(4);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
