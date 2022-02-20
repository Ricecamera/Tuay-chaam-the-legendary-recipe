using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaamrender_temp : PakRender
{
    protected override void Start()
    {
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        base.Start();
        //!
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 0, skillIcon2));
        skill.Add(new VanillaHealOne("VH1", "HealOneAlliance", "Target one alliance and heal it.", 0, skillIcon1));
        skill.Add(new VanillaBuffAtkAll("VBAA", "BuffAtkAllAlliance", "Buff all alliance to increase their strenght for the duration of this battle.", 0, skillIcon3));
        skill.Add(new VanillaGainSPOne("VGSP1", "GainSPOneAlliance", "Target one alliance and it gain 1 SP.", 0, skillIconUnti));
    }
}
