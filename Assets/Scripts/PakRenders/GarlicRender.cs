public class GarlicRender : PakRender
{

    protected override void Start()
    {
        base.Start();
        skill.Add(new VanillaBuffAtkOne("buff2", "BuffoneEnemy", "", 0));
        skill.Add(new VanillaHealAll("heal3", "HealAllEnemy", "", 0));
    }

}
