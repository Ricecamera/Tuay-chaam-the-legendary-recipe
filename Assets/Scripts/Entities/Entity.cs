using System;
using UnityEngine;

[Serializable]
public class BonusStats {
    public int addHp;
    public int addAtk;
    public int addDef;
}

// Base class for all enitities in this game
public class Entity : ScriptableObject {
    //fields
    [SerializeField]
    protected string entityId, entityName;

    [TextArea]
    [SerializeField]
    protected string description;

    [SerializeField]
    protected int atk, def, maxHp;

    [SerializeField]
    public Sprite image;                             // sprite of the character

    [SerializeField]
    protected BonusStats bonusStatsPerLvl;                 // the bonus stats that will add the character's base stats when it levels up


    // Getter Setter
    public string EntityId {
        get { return this.entityId; }
        set { this.entityId = value; }
    }

    public string EntityName {
        get { return this.entityName; }
        set { this.entityName = value; }
    }

    public string Description {
        get { return this.description; }
        set { this.description = value; }
    }
    
    public int Atk {
        get { return this.atk; }
        set { this.atk = value; }
    }

    public int Def {
        get { return this.def; }
        set { this.def = value; }
    }

    public Sprite Image {
        get { return this.image; }
        set { this.image = value; }
    }

    public BonusStats BonusStatsPerLvl {
        get { return this.bonusStatsPerLvl; }
        set { this.bonusStatsPerLvl = value; }
    }

    public int MaxHp {
        get { return this.maxHp; }
        set { this.maxHp = value; }
    }
}
