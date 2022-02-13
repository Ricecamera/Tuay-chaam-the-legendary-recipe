using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    //fields
    protected string skillId;

    protected string skillName;

    [TextArea]
    protected string description;

    protected int cooldown;

    //Constructortor
    public Skill(string skillId, string skillName, string description, int cooldown){
        this.skillId = skillId;
        this.skillName = skillName;
        this.description = description;
        this.cooldown = cooldown;
    }

    //getters setters
    public string SkillId {
        get {return this.skillId;}
        set {this.skillId = value;}
    }
    public string SkillNamed {
        get {return this.skillName;}
        set {this.skillName = value;}
    }
    public string Description {
        get {return this.description;}
        set {this.description = value;}
    }public int Cooldown {
        get {return this.cooldown;}
        set {this.cooldown = value;}
    }
}
