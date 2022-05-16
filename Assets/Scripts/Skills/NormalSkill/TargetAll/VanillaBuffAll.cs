using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;


[CreateAssetMenu(fileName = "Buff all", menuName = "Assets/skill/Buff all")]
public class VanillaBuffAll : RangeSkill
{

    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    private StatusBuff buff;
    public override void Execute(List<PakRender> targets, PakRender self)
    {
        foreach (PakRender e in targets)
        {
            e.AddBuff(buff);
            ParticleSystem vfx = Instantiate(particles, e.GetPosition(), Quaternion.identity);
            Destroy(vfx.gameObject, vfx.main.duration);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buff", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
