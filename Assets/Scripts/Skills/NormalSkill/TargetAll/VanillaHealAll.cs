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
            e.healthSystem.CurrentHp += healValue;    //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.Heal(healValue);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
