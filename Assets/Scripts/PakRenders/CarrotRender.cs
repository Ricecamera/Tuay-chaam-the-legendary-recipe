public class CarrotRender : PakRender
{

    protected override void Start()
    {
        base.Start();
        skill.Add(new VanillaAttackAll("atk2", "AttackAllEnemy", "", 0));
        skill.Add(new VanillaHealOne("heal3", "HealOneEnemy", "", 0));
    }






}
