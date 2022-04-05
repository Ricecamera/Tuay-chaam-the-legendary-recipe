using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Attack one", menuName = "Assets/skill/attack one")]
public class VanillaAttackOne : MeleeSkill {

    public float damageRatio = 1f;

    public override void Execute(List<PakRender> target, PakRender caller) {
        int atkValue = (int) (caller.currentAtk * damageRatio);

        target[0].takeDamage(atkValue);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOne", soundBank[0].GetComponent<BattleSound>().clips);
    }
}