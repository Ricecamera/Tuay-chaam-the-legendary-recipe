using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;


[CreateAssetMenu(fileName = "Buff all", menuName = "Assets/skill/Buff all")]
public class VanillaBuffAll : RangeSkill {
    
    [SerializeField]
    private StatusBuff buff;
    public override void Execute(List<PakRender> targets, PakRender self) {
        foreach (PakRender e in targets) {
            e.AddBuff(buff);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
