using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaAttackAllPerforming : Performable
{
    public void performSkill(List<PakRender> target, PakRender self)
    {
        int damage;
        int atkValue = self.currentAtk;
        foreach (PakRender e in target)
        {
            // damage = atkValue*(100/(100+e.pak.Def));
            damage = (int) (atkValue * 0.65f * (100f / (100f + e.currentDef)));
            if (damage <= 0)
            {
                damage = 1;
            }
            // e.pak.-=damage;               //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            // if(e.pak.Hp<=0) e.pak.Hp=0;     //use this function if hp in Entity matter. If not, only use the heal and damage function from health system.
            e.healthSystem.TakeDamage(damage);
            e.switchMat();
        }
        return;   
    }
}
