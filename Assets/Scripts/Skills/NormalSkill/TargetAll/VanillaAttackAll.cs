using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack all target", menuName = "Assets/skill/attack all")]
public class VanillaAttackAll : MeleeSkill
{

    public float damageRatio;

    public override void Execute(List<PakRender> target, PakRender self)
    {
        int atkValue = (int)(self.currentAtk * damageRatio);
        foreach (PakRender e in target)
        {
            e.takeDamage(atkValue);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitAll", soundBank[0].GetComponent<BattleSound>().clips);
    }
}
