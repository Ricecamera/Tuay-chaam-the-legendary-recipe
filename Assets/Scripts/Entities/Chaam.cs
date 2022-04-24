using UnityEngine;
using TasteSystem;

// Chaam is the special character that player must have one in the team
[CreateAssetMenu(fileName = "New Chaam", menuName = "Assets/Entity/Chaam")]
public class Chaam : Entity {
    [SerializeField]
    protected int maxGauge;

    protected Taste taste = Taste.BLAND;

    public int MaxGauge {
        get { return this.maxGauge; }
        set { this.maxGauge = value; }
    }

    public Taste Taste {
        get { return this.taste; }
        set { this.taste = value; }
    }
}
