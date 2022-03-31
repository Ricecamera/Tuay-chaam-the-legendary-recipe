using UnityEngine;
using TasteSystem;

// Pak is the playable character
[CreateAssetMenu(fileName = "New Pak", menuName = "Assets/Entity/Pak")]
public class Pak : Entity {
    //fields
    [SerializeField]
    protected int maxSp;

    [SerializeField]
    protected Taste taste;

    // Getter Setter
    public int MaxSp {
        get { return this.maxSp; }
        set { this.maxSp = value; }
    }

    public Taste Taste {
        get { return this.taste; }
        set { this.taste = value; }
    }
}
