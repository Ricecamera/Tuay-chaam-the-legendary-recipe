using UnityEngine;
public class GarlicRender : PakRender
{

    protected override void Start()
    {
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;
        base.Start();
        //!
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 0, skillIcon1));
        skill.Add(new VanillaBuffDefOne("VBD1", "BuffDefOneAlliance", "Target one alliance and buff its defense for the duration of this battle.", 0, skillIcon2));
        skill.Add(new Buum("B:)", "AttackWholeField", "Dealt a great amount of damages to the whole field at a cost of sacrifices the Pak who use this action.", 0, skillIconUnti));
        skill.Add(new VanillaBuffDefAll("VBDA", "BuffDefAllAlliance", "Buff all alliance to increase their defense for the duration of this battle.", 0, skillIcon3));

    }

}
