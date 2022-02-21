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

    protected int maxCooldown;

    //Constructortor
    public Skill(string skillId, string skillName, string description, int cooldown, Sprite icon, string actionType)
    {
        this.skillId = skillId;
        this.skillName = skillName;
        this.description = description;
        this.maxCooldown = cooldown;
        this.cooldown = 0;
        this.icon = icon;
        this.actionType = actionType;
    }

    //functions
    public void performSkill(Skill skill, PakRender caller, List<PakRender> target)
    {
  
        switch (skill.skillId)
        {
            case "VA1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackOneEnemy is called");
                VanillaAttackOne vskill = (VanillaAttackOne)skill;
                vskill.AttackOneEnemy(target, caller);
                cooldown = maxCooldown;
                break;

            case "VAA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackAllEnemy is called");
                VanillaAttackAll vskill2 = (VanillaAttackAll)skill;
                vskill2.AttackAllEnemy(target, caller);
                cooldown = maxCooldown;
                break;

            case "VBA1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkOneAlliance is called");
                VanillaBuffAtkOne vskill3 = (VanillaBuffAtkOne)skill;
                vskill3.BuffAtkOneAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VBD1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefOneAlliance is called");
                VanillaBuffDefOne vskill4 = (VanillaBuffDefOne)skill;
                vskill4.BuffDefOneAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VGSP1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GainSPOneAlliance is called");
                VanillaGainSPOne vskill5 = (VanillaGainSPOne)skill;
                vskill5.GainSPOneAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VH1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealOneAlliance is called");
                VanillaHealOne vskill6 = (VanillaHealOne)skill;
                vskill6.HealOneAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VBAA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkAllAlliance is called");
                VanillaBuffAtkAll vskill7 = (VanillaBuffAtkAll)skill;
                vskill7.BuffAtkAllAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VBDA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefAllAlliance is called");
                VanillaBuffDefAll vskill8 = (VanillaBuffDefAll)skill;
                vskill8.BuffDefAllAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VGSPA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GainSPAllAlliance is called");
                VanillaGainSPAll vskill9 = (VanillaGainSPAll)skill;
                vskill9.GainSPAllAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "VHA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealAllAlliance is called");
                VanillaHealAll vskill10 = (VanillaHealAll)skill;
                vskill10.HealAllAlliance(target, caller);
                cooldown = maxCooldown;
                break;

            case "B:)":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ ActionBuum is called");
                Buum vskill11 = (Buum)skill;
                vskill11.AttackWholeField(target, caller);
                cooldown = maxCooldown;
                break;

            default:
                Debug.Log("Skill ID not matched");
                Debug.LogError("Skill not being call.");
                break;
        }
    }

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
        set { 
            if (value < 0)
                cooldown = 0;
            else if (value > maxCooldown)
                cooldown = maxCooldown;
            else
                cooldown = value;
                }
    }

    public int MaxCooldown {
        get { return maxCooldown;}
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
