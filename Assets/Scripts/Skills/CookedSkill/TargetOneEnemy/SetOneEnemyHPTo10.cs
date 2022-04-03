using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOneEnemyHPTo10Performing : MeleeSkill
{
    public override void Execute(List<PakRender> target, PakRender self)
    {
        if(target[0].healthSystem.CurrentHp <280) target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp-10);
        else target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp/2);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOneHard", soundBank[0].GetComponent<BattleSound>().clips);
        
        return;
    }
}
