using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillExecutor
{
    //Performable
    [SerializeField]
    private SkillObj skill;

    private int cooldown;

    private int maxCooldown;

    public event Action OnFinishExecute;

    //Constructortor
    public SkillExecutor(Action onFinishExecute) {
        this.cooldown = 0;
        this.maxCooldown = skill.maxCooldown;
        this.OnFinishExecute += onFinishExecute;
    }

    public void performSkill(List<PakRender> target, PakRender self) {
        skill.performSkill(target, self, () => {
            // Complete callback
            OnFinishExecute.Invoke();
            cooldown = maxCooldown;
        });
    }

    public void SetSkill(SkillObj newSkill) {
        this.skill = newSkill;
    }

    //Getters, Setters
    public string SkillId {
        get { 
            return this.skill.skillId; 
        }
 
    }
    public string SkillNamed {
        get { 
            return this.skill.skillName; 
        }
    }
    public string Description {
        get { 
            return this.skill.description; 
        }
    }
    public int Cooldown {
        get { 
            return this.cooldown; 
        }
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
        get { return maxCooldown; }
    }
    public Sprite Icon {
        get {
            return this.skill.icon;
        }
    }

    public string ActionType {
        get { 
            return this.skill.actionType;
        }
    }
}
