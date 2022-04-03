using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VanillaAttackOne : ScriptableObject, Performable {

    public void Execute(List<PakRender> target, PakRender caller) {
        int damage;
        int atkValue = caller.currentAtk;
        damage = (int) (atkValue * (decimal) (100f / (100f + target[0].currentDef)));

        target[0].healthSystem.TakeDamage(damage);
        target[0].switchMat();

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOne", soundBank[0].GetComponent<BattleSound>().clips);
    }
}