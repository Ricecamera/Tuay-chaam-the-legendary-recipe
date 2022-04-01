using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaAttackOnePerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {  
        int damage;
        int atkValue = self.currentAtk;
        damage = (int)(atkValue * (decimal)(100f / (100f + target[0].currentDef)));

        //target.pak.Hp-=damage;                  //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        //if(target.pak.Hp<=0) target.pak.Hp=0;   //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
        target[0].healthSystem.TakeDamage(damage);
        target[0].switchMat();

        //add sound effect
        GameObject[] soundBank = GameObject.FindGameObjectsWithTag("SoundBank");
        SoundManager.Instance.PlaySound("HitOne", soundBank[0].GetComponent<BattleSound>().clips);
        return;
    }
}
