using System;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;

public abstract class SkillObj: ScriptableObject {

    //fields
    public string skillId;

    public string skillName;

    public Sprite icon;

    [TextArea]
    public string description;

    public string actionType;

    public int maxCooldown;

    public abstract void performSkill(List<PakRender> target, PakRender self, Action onComplete);

}
