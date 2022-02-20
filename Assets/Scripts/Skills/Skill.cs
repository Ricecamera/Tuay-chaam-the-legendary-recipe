using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    //fields
    protected string skillId;

    protected string skillName;

    protected Sprite icon;

    [TextArea]
    protected string description;
    protected int cooldown;

    protected string actionType;

    //Constructortor
    public Skill(string skillId, string skillName, string description, int cooldown, Sprite icon, string actionType)
    {
        this.skillId = skillId;
        this.skillName = skillName;
        this.description = description;
        this.cooldown = cooldown;
        this.icon = icon;
        this.actionType = actionType;
    }

    //functions
    // public void performSkill(Skill skill, Caller, target){
    //     switch (skill.skillId)
    //     {   
    //         case "VA1":
    //             VanillaAttackOne vskill = (VanillaAttackOne)skill;
    //             vskill.AttackOneEnemy()
    //             break;

    //         case "VAA":

    //             break;

    //         default:

    //             break;
    //     }
    // }

    //getters setters
    public string SkillId
    {
        get { return this.skillId; }
        set { this.skillId = value; }
    }
    public string SkillNamed
    {
        get { return this.skillName; }
        set { this.skillName = value; }
    }
    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }
    public int Cooldown
    {
        get { return this.cooldown; }
        set { this.cooldown = value; }
    }
    public Sprite Icon
    {
        get { return this.icon; }
        set { this.icon = value; }
    }

    public string ActionType
    {
        get { return this.actionType; }
    }
}
