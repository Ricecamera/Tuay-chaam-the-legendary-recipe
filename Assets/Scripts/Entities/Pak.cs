using UnityEngine;

// Pak is the playable character
[CreateAssetMenu(fileName = "New Pak", menuName = "Assets/Entity/Pak")]
public class Pak : Character {
    //fields
    protected int sp;
    // Getter Setter
    public int Sp {
        get { return this.sp; }
        set { this.sp = value; }
    }

    // TODO: Add Pak related logic and functionality
}
