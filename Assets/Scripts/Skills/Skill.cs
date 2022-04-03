using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;

public class Skill
{
    //Skill Type
    public enum SkillNation
    {
        NORMAL, COOKED
    }
    //Performable
    protected Performable performAct;
    //fields
    protected string skillId;

    protected SkillNation skillNation;

    protected string skillName;

    protected Sprite icon;

    [TextArea]
    protected string description;

    protected int cooldown;

    protected string actionType;

    protected int maxCooldown;

    public event Action OnFinishExecute;

    protected string animationType;

    //delegates
    public Action<List<PakRender>, PakRender> doTheSkill;

    //Constructortor
    public Skill(string skillId, string skillName, string description, int cooldown, Sprite icon, string actionType, Performable performAct, string animationType, SkillNation skillNation = SkillNation.NORMAL)
    {
        this.skillId = skillId;
        this.skillName = skillName;
        this.description = description;
        this.maxCooldown = cooldown;
        this.cooldown = 0;
        this.icon = icon;
        this.actionType = actionType;
        this.performAct = performAct;
        this.animationType = animationType;
        this.doTheSkill += performAct.performSkill;
        this.skillNation = skillNation;
    }

    //functions
    public void performSkill(PakRender caller, List<PakRender> target)
    {
        if (string.Equals(this.animationType, "buffOneAlliance") || string.Equals(this.animationType, "buffAllAlliances") || string.Equals(this.animationType, "attackWholeField"))
        {
            Buff(caller, target, this.doTheSkill);
        }
        else if (string.Equals(this.animationType, "attackOneEnemy") || string.Equals(this.animationType, "attackAllEnemies"))
        {
            Attack(caller, target, this.doTheSkill);
        }
        else
        {
            Debug.Log("Wrong animation type of skill");
        }
        if (skillNation == SkillNation.COOKED)
        {
            List<PakRender> pakTeam = CharacterManager.instance.getTeamHolders(0);
            foreach (PakRender x in pakTeam)
            {
                if (x.CompareTag("Chaam") && x.healthSystem.CurrentHp > 0)
                {
                    ChaamRender nongChaam = (ChaamRender)x;
                    Debug.Log("b/f");
                    Debug.Log(nongChaam.getGuage());
                    nongChaam.setGuage(0);
                    Debug.Log(nongChaam.getGuage());
                    Debug.Log("a/f");
                    break;
                }
            }
        }
    }

    // public void performSkill( PakRender caller, List<PakRender> target)
    // {
    //     switch (this.skillId)
    //     {
    //         case "VA1":
    //             // Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackOneEnemy is called");
    //             // VanillaAttackOne vskill = (VanillaAttackOne) this;
    //             Attack(caller, target, this.doTheSkill);
    //             break;

    //         case "VAA":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ AttackAllEnemy is called");
    //             VanillaAttackAll vskill2 = (VanillaAttackAll) this;
    //             Attack(caller, target, this.doTheSkill);
    //             break;

    //         case "VBA1":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkOneAlliance is called");
    //             VanillaBuffAtkOne vskill3 = (VanillaBuffAtkOne) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "VBD1":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefOneAlliance is called");
    //             VanillaBuffDefOne vskill4 = (VanillaBuffDefOne) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "VH1":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealOneAlliance is called");
    //             VanillaHealOne vskill6 = (VanillaHealOne) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "VBAA":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffAtkAllAlliance is called");
    //             VanillaBuffAtkAll vskill7 = (VanillaBuffAtkAll) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "VBDA":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BuffDefAllAlliance is called");
    //             VanillaBuffDefAll vskill8 = (VanillaBuffDefAll) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "VHA":
    //             Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ HealAllAlliance is called");
    //             VanillaHealAll vskill10 = (VanillaHealAll) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         case "B:)":
    //             // Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ ActionBuum is called");
    //             // Buum vskill11 = (Buum) this;
    //             Buff(caller, target, this.doTheSkill);
    //             break;

    //         default:
    //             Debug.Log("Skill ID not matched");
    //             Debug.LogError("Skill not being call.");
    //             break;
    //     }
    // }

    private void Attack(PakRender caller, List<PakRender> target, Action<List<PakRender>, PakRender> toBeCall)
    {
        Vector3 targetPos = target[0].GetPosition();
        caller.Attack(targetPos, () =>
        {
            // Callback of attack effect
            toBeCall(target, caller);
        },
        () =>
        {
            // Complete callback
            OnFinishExecute.Invoke();
            cooldown = maxCooldown;
        });
    }

    private void Buff(PakRender caller, List<PakRender> target, Action<List<PakRender>, PakRender> toBeCall)
    {
        caller.RangedBuff(this.skillId,
            () =>
            {
                toBeCall(target, caller);
            },
            () =>
            {
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
        set
        {
            if (value < 0)
                cooldown = 0;
            else if (value > maxCooldown)
                cooldown = maxCooldown;
            else
                cooldown = value;
        }
    }

    public int MaxCooldown
    {
        get { return maxCooldown; }
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

    public SkillNation getSkillNation
    {
        get { return this.skillNation; }
    }
}
