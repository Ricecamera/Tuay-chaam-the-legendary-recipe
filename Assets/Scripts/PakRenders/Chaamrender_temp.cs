using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;
public class Chaamrender_temp : PakRender
{
    protected override void Start()
    {
        base.Start();
        try {
            Sprite skillIcon2 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
            Sprite skillIcon1 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
            Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
            Sprite skillIconUnti = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        
            //!
            skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, skillIcon2));
            skill.Add(new VanillaBuffAtkAll("VBAA", "BuffAtkAllAlliance", "Buff all alliance to increase their strength for the duration of this battle.", 3, skillIcon3));
            skill.Add(new VanillaHealAll("VHA", "HealAllAlliance", "Heal all alliances.", 4, skillIcon1));
            skill.Add(new VanillaGainSPOne("VGSP1", "GainSPOneAlliance", "Target one alliance and it gain 1 SP.", 3, skillIconUnti));
        }
        catch {
            Debug.Log("skill icons not found!!");
            Sprite dummyImage = BattleManager.instance.dummySkill;

            //!
            skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, dummyImage));
            skill.Add(new VanillaBuffAtkAll("VBAA", "BuffAtkAllAlliance", "Buff all alliance to increase their strength for the duration of this battle.", 3, dummyImage));
            skill.Add(new VanillaHealAll("VHA", "HealAllAlliance", "Heal all alliances.", 4, dummyImage));
            skill.Add(new VanillaGainSPOne("VGSP1", "GainSPOneAlliance", "Target one alliance and it gain 1 SP.", 3, dummyImage));

        }
    }
}
