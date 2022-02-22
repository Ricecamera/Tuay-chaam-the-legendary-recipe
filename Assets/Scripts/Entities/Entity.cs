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
    protected int baseAtk, baseDef, maxHp, baseSpeed;

    [SerializeField]
    public Sprite image;                             // sprite of the character

    [SerializeField]
    protected BonusStats bonusStatsPerLvl;              // the bonus stats that will add the character's base stats when it levels up


    // Getter Setter
    public string EntityId {
        get { return this.entityId; }
    }

    public string EntityName {
        get { return this.entityName; }
    }

    public string Description {
        get { return this.description; }
    }
    
    public int BaseAtk {
        get { return this.baseAtk; }
    }

    public int BaseDef {
        get { return this.baseDef; }
    }

    public Sprite Image {
        get { return this.image; }
    }

    public BonusStats BonusStatsPerLvl {
        get { return this.bonusStatsPerLvl; }
    }

    public int MaxHp {
        get { return this.maxHp; }
    }

    public int BaseSpeed {
        get { return this.baseSpeed; }
    }
}
