using UnityEngine;

public class EggplantRender : PakRender
{

    protected override void Start()
    {
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
        base.Start();
        //!
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, skillIcon1));
        skill.Add(new VanillaAttackAll("VAA", "AttackAllEnemy", "Attack all enemies at once.", 3, skillIcon2));
        skill.Add(new VanillaBuffAtkOne("VBA1", "BuffAtkOneAlliance", "Target one alliance and buff its strength for the duration of this battle.", 2, skillIcon3));
        skill.Add(new VanillaBuffAtkAll("VBAA", "BuffAtkAllAlliance", "Buff all alliance to increase their strenght for the duration of this battle.", 5, skillIconUnti));
    }
}
