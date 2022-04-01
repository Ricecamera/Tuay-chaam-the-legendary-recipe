using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaBuffDefAllPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        int buffValue = (self.currentDef) / 6;
        foreach (PakRender e in target)
        {
            //TODO List
                /*
                    TODO1: Add buff element to pak with expried date
                    TODO2: delete the direct add buff value
                    TODO3: may change buffValue formula.
                */
            e.currentDef += buffValue;
            e.defBuffVfx.Play();
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
