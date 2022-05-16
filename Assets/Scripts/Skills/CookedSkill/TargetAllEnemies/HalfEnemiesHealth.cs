using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HalfEnemiesHealth", menuName = "Assets/Cook skill/HalfEnemiesHealth")]
public class HalfEnemiesHealth : MeleeSkill
{
    public override void Execute(List<PakRender> target, PakRender self)
    {
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            int damage = (int)(e.healthSystem.MaxHp / 1.35);
            if (damage <= 0)
            {
                damage = 1;
            }
            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
            e.switchMat(damage, false);
        }

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOneHard", soundBank[0].GetComponent<BattleSound>().clips);

        return;
    }
}
