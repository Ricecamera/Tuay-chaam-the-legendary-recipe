using System;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;

public abstract class SkillObj: ScriptableObject {

    public enum SkillNation {
        NORMAL, COOKED
    }

    //fields
    public string skillId;

    public string skillName;

    public SkillNation skillNation;

    public Sprite icon;

    [TextArea]
    public string description;

    public string actionType;

    public int maxCooldown;

    public abstract void performSkill(List<PakRender> target, PakRender self, Action onComplete);
    public abstract void Execute(List<PakRender> targets, PakRender self);
}
