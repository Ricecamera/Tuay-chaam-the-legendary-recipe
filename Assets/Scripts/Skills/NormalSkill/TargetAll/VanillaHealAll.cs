using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal all", menuName = "Assets/skill/Heal all")]
public class VanillaHealAll : RangeSkill
{
    public float healRatio = 0.15f;

    public override void Execute(List<PakRender> target, PakRender self)
    {
        int healValue = (int) ((float) self.healthSystem.MaxHp * healRatio);
        foreach (PakRender e in target)
        {
            e.healthSystem.heal(healValue);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
