using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal one", menuName = "Assets/skill/Heal one")]
public class VanillaHealOne : RangeSkill
{
    public float healRatio = .25f;
    public override void Execute(List<PakRender> target, PakRender self)
    {
        int healValue = (int)((float)self.healthSystem.MaxHp * healRatio);
        target[0].healthSystem.Heal(healValue);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Heal", soundBank[0].GetComponent<BattleSound>().clips);
    }
}
