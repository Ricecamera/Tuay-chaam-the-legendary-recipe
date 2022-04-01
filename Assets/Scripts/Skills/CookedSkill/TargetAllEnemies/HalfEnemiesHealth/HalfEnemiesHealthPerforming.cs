using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfEnemiesHealthPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            int damage = (int) (e.healthSystem.CurrentHp/2);
            if (damage <= 0)
            {
                damage = 1;
            }
            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }
        
        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOneHard", soundBank[0].GetComponent<BattleSound>().clips);

        return;
    }
}
