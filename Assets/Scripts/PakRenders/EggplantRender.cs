public class EggplantRender : PakRender
{

    protected override void Start()
    {
        base.Start();
        skill.Add(new VanillaAttackOne("atk2", "AtkoneEnemy", "", 0));
        skill.Add(new VanillaAttackAll("heal3", "AttackAllEnemy", "", 0));
    }
}
