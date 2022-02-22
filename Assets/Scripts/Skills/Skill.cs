using System;
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

    public event Action OnFinishExecute;

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
    public void performSkill( PakRender caller, List<PakRender> target)
    {
        switch (this.skillId)
        {
            case "VA1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackOneEnemy is called");
                VanillaAttackOne vskill = (VanillaAttackOne) this;
                Attack(caller, target, vskill.AttackOneEnemy);
                break;

            case "VAA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackAllEnemy is called");
                VanillaAttackAll vskill2 = (VanillaAttackAll) this;
                Attack(caller, target, vskill2.AttackAllEnemy);
                break;

            case "VBA1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkOneAlliance is called");
                VanillaBuffAtkOne vskill3 = (VanillaBuffAtkOne) this;
                Buff(caller, target, vskill3.BuffAtkOneAlliance);
                break;

            case "VBD1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefOneAlliance is called");
                VanillaBuffDefOne vskill4 = (VanillaBuffDefOne) this;
                Buff(caller, target, vskill4.BuffDefOneAlliance);
                break;

            case "VGSP1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GainSPOneAlliance is called");
                VanillaGainSPOne vskill5 = (VanillaGainSPOne) this;
                Buff(caller, target, vskill5.GainSPOneAlliance);
                break;

            case "VH1":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealOneAlliance is called");
                VanillaHealOne vskill6 = (VanillaHealOne) this;
                Buff(caller, target, vskill6.HealOneAlliance);
                break;

            case "VBAA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkAllAlliance is called");
                VanillaBuffAtkAll vskill7 = (VanillaBuffAtkAll) this;
                Buff(caller, target, vskill7.BuffAtkAllAlliance);
                break;

            case "VBDA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefAllAlliance is called");
                VanillaBuffDefAll vskill8 = (VanillaBuffDefAll) this;
                Buff(caller, target, vskill8.BuffDefAllAlliance);
                break;

            case "VGSPA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GainSPAllAlliance is called");
                VanillaGainSPAll vskill9 = (VanillaGainSPAll) this;
                Buff(caller, target, vskill9.GainSPAllAlliance);
                break;

            case "VHA":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealAllAlliance is called");
                VanillaHealAll vskill10 = (VanillaHealAll) this;
                Buff(caller, target, vskill10.HealAllAlliance);
                break;

            case "B:)":
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ ActionBuum is called");
                Buum vskill11 = (Buum) this;
                Buff(caller, target, vskill11.AttackWholeField);
                break;

            default:
                Debug.Log("Skill ID not matched");
                Debug.LogError("Skill not being call.");
                break;
        }
    }

    private void Attack(PakRender caller, List<PakRender> target, Action<List<PakRender>, PakRender> toBeCall) {
        Vector3 targetPos = target[0].GetPosition();
        caller.Attack(targetPos, () => {
            // Callback of attack effect
            toBeCall(target, caller);
        },
        () => {
            // Complete callback
            OnFinishExecute.Invoke();
            cooldown = maxCooldown;
        });
    }

    private void Buff(PakRender caller, List<PakRender> target, Action<List<PakRender>, PakRender> toBeCall) {
        caller.RangedBuff(
            () => {
                toBeCall(target, caller);
            },
            () => {
                // Complete callback
                OnFinishExecute.Invoke();
                cooldown = maxCooldown;
            });
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
