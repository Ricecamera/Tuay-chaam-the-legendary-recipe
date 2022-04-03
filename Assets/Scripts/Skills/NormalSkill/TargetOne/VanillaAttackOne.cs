using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Attack one", menuName = "Assets/skill/attack one")]
public class VanillaAttackOne : MeleeSkill {

    public float damageRatio = 1f;

    public override void Execute(List<PakRender> target, PakRender caller) {
        int damage;
        float atkValue = caller.currentAtk * damageRatio;
        damage = (int) (atkValue * (float) (100f / (100f + target[0].currentDef)));

        target[0].healthSystem.TakeDamage(damage);
        target[0].switchMat();

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOne", soundBank[0].GetComponent<BattleSound>().clips);
    }
}