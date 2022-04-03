using UnityEngine;
public class TonhomRender : PakRender {
    protected override void Start() {
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;

        base.Start();
        //skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "Do damage to one enemy.", 1, skillIcon1));
        //skill.Add(new VanillaAttackAll("VAA", "AttackAllEnemy", "Attack all enemies at once.", 3, skillIcon2));
        //skill.Add(new VanillaHealOne("VH1", "HealOneAlliance", "Target one alliance and heal it.", 2, skillIcon3));
        //skill.Add(new VanillaHealAll("VHA", "HealAllAlliance", "Heal all alliances at once.", 4, skillIconUnti));
    }
}
