using UnityEngine;
public class ChaamRender : PakRender
{
    private int guage;
    protected override void Start()
    {
        base.Start();
        this.guage = 0;
        Sprite skillIcon2 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk4", typeof(Sprite)) as Sprite;
        Sprite skillIcon3 = Resources.Load("SkillIcons/sk1", typeof(Sprite)) as Sprite;
        Sprite skillIconUnti = Resources.Load("SkillIcons/sk2", typeof(Sprite)) as Sprite;

        //!
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "Do damage to one enemy.", 1, skillIcon2));
        skill.Add(new VanillaBuffAtkAll("VBAA", "BuffAtkAllAlliance", "Buff all alliance to increase their strength for the duration of this battle.", 3, skillIcon3));
        skill.Add(new VanillaHealAll("VHA", "HealAllAlliance", "Heal all alliances.", 4, skillIcon1));
        skill.Add(new VanillaGainSPOne("VGSP1", "GainSPOneAlliance", "Target one alliance and gain it 1 SP.", 3, skillIconUnti));
    }

    public void addGuage(int plus)
    {
        if (this.guage + plus <= 100)
        {
            this.guage = this.guage + plus;
        }
        else
        {
            this.guage = 100;
        }
    }

    public int getGuage()
    {
        return this.guage;
    }

    public void setGuage(int guage)
    {
        if (guage <= 100)
        {
            this.guage = guage;
        }
        else
        {
            Debug.Log("False set guage of Chaam");
        }
    }
}
