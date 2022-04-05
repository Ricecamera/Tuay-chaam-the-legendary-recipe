using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeSkill : SkillObj {

    public override void performSkill(List<PakRender> targets, PakRender self, Action onComplete) { 
        if (targets.Count == 0)
            throw new Exception("Target is not provided");

        self.RangedBuff(this.skillId, () => {
            // Callback of attack effect
            Execute(targets, self);
        },
        onComplete
        );
    }

}