using UnityEngine;

// Chaam is the special character that player must have one in the team
[CreateAssetMenu(fileName = "New Chaam", menuName = "Assets/Entity/Chaam")]
public class Chaam : Entity {
    [SerializeField]
    protected int maxGauge;

    public int MaxGauge {
        get { return this.maxGauge; }
        set { this.maxGauge = value; }
    }

    // TODO: Add Chaam related logic and functionality
}
