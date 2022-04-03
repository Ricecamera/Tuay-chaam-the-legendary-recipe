using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack all target", menuName = "Assets/skill/attack all")]
public class VanillaAttackAll : MeleeSkill {

    public float damageRatio;

    public override void Execute(List<PakRender> target, PakRender self)
    {
        int damage;
        int atkValue = self.currentAtk;
        foreach (PakRender e in target)
        {
            damage = (int) (atkValue * damageRatio * (100f / (100f + e.currentDef)));
            if (damage <= 0)
            {
                damage = 1;
            }
    
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitAll", soundBank[0].GetComponent<BattleSound>().clips);
    }
}
