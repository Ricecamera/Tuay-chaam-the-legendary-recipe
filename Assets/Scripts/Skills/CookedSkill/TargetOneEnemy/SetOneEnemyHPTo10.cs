using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetOneEnemyHPTo10", menuName = "Assets/Cook skill/SetOneEnemyHPTo10")]
public class SetOneEnemyHPTo10 : MeleeSkill
{
    public override void Execute(List<PakRender> target, PakRender self)
    {
        // if(target[0].healthSystem.CurrentHp <280) target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp-10);
        // else target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp/2);
        target[0].healthSystem.TakeDamage(target[0].healthSystem.CurrentHp-10);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOneHard", soundBank[0].GetComponent<BattleSound>().clips);

        return;
    }
}
