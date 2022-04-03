using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeSkill : SkillObj {

    public List<Performable> effectLists;

    public override void performSkill(List<PakRender> targets, PakRender self, Action onComplete) {
        if (targets.Count == 0)
            throw new Exception("Target is not provided");

        Vector3 targetPos = targets[0].GetPosition();
        self.Attack(targetPos, () => {
            // Callback of attack effect
            Execute(targets, self);
        },
        onComplete
        );
    }
}
