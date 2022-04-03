using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaBuffOne : ScriptableObject, Performable {
    public void Execute(List<PakRender> target, PakRender self) {
        //TODO List
        /*
            TODO1: Add buff element to pak with expried date
            TODO2: delete the direct add buff value
            TODO3: may add buffValue formula.
        */
        target[0].currentDef += self.currentDef / 4;
        target[0].defBuffVfx.Play();

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}