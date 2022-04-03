using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;

[CreateAssetMenu(fileName = "Buff one", menuName = "Assets/skill/Buff one")]
public class VanillaBuffOne : RangeSkill {

    [SerializeField]
    private StatusBuff buff;

    public override void Execute(List<PakRender> target, PakRender self) {

        target[0].AddBuff(buff);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
    }
}