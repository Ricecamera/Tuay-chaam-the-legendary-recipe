using TasteSystem;
using UnityEngine;

// Chracter is the classes for normal enemies
// Store skilles and taste of the specific character
[CreateAssetMenu(fileName = "New Character", menuName = "Assets/Entity/Character")]
public class Character : Entity {
    //fields
    [SerializeField]
    protected int maxSp;

    [SerializeField]
    protected Taste taste;


    public int MaxSp {
        get { return this.maxSp; }
        set { this.maxSp = value; }
    }

    public Taste Taste {
        get { return this.taste; }
        set { this.taste = value; }
    }
}
