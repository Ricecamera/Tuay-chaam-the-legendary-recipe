using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buum", menuName = "Assets/skill/Buum")]
public class Buum : RangeSkill
{
    public float damageRatio = 0.2f; //0.6f;


    public override void Execute(List<PakRender> target, PakRender self)
    { //target can be the list of all Pakrender in the fighting scene.

        self.switchMat(0,2);       
        int damage = (int)((float)self.healthSystem.MaxHp * damageRatio);
        target.Remove(self);
        foreach (PakRender e in target)
        {
            if (damage - e.currentDef <= 0) damage = UnityEngine.Random.Range(50, 60);
            if (e.healthSystem.IsAlive)
            {
                e.healthSystem.TakeDamage(damage);
            }
        }
        self.healthSystem.TakeDamage(damage);

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("Buum", soundBank[0].GetComponent<BattleSound>().clips);
    }

}