using UnityEngine;
public class ChaamRender : PakRender
{
    private int guage;
    protected override void Start()
    {
        base.Start();
        this.guage = 0;
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
