using UnityEngine;

public class TonhomRender : PakRender
{
    // Start is called before the first frame update
    protected override void Start() {
       
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
        base.Start();

        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, skillIcon1));
        skill.Add(new VanillaHealOne("VH1", "HealOneAlliance", "Target one alliance and heal it.", 2, skillIcon2));
        skill.Add(new VanillaBuffDefAll("VBDA", "BuffDefAllAlliance", "Buff all alliance to increase their defense for the duration of this battle.", 4, skillIcon3));
    }
}
