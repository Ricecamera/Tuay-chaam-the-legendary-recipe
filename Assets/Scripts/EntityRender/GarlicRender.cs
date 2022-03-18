using UnityEngine;
using BattleScene;
using System.Linq;

public class GarlicRender : PakRender
{

    protected override void Start()
    {
        base.Start();
        try {
            int fileCount = Resources.LoadAll<Sprite>("SkillIcons").ToList().Count;
            Debug.Log("skill icon files in folder: " + fileCount);

            Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
            Sprite skillIcon2 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
            Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
            Sprite skillIconUnti = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;

            //!
            skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, skillIcon1));
            skill.Add(new VanillaBuffDefOne("VBD1", "BuffDefOneAlliance", "Target one alliance and buff its defense for the duration of this battle.", 2, skillIcon2));
            skill.Add(new Buum("B:)", "AttackWholeField", "Dealt a great amount of damages to the whole field at a cost of sacrifices the Pak who use this action.", 5, skillIconUnti));
            skill.Add(new VanillaBuffDefAll("VBDA", "BuffDefAllAlliance", "Buff all alliance to increase their defense for the duration of this battle.", 4, skillIcon3));
        }
        catch {

            Debug.LogError("Can't load images");
        }
    }

}
