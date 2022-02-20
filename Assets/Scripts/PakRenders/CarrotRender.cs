using UnityEngine;
public class CarrotRender : PakRender
{
    protected override void Start()
    {
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;

        base.Start();
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 0, skillIcon1));
        skill.Add(new VanillaAttackAll("VAA", "AttackAllEnemy", "Attack all enemies at once.", 0, skillIcon2));
        skill.Add(new VanillaHealOne("VH1", "HealOneAlliance", "Target one alliance and heal it.", 0, skillIcon3));
        skill.Add(new VanillaHealAll("VHA", "HealAllAlliance", "Heal all alliances at once.", 0, skillIconUnti));

        // Debug.Log(base.skill[0]);
        // Debug.Log(base.skill[1]);
        // Debug.Log(base.skill[2]);
    }
}
