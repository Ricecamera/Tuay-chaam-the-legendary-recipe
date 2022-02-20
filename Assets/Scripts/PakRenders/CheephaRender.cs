using UnityEngine;
public class CheephaRender : PakRender
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
        skill.Add(new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 0, skillIcon1));
    }

}
