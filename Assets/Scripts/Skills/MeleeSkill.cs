using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeSkill : SkillObj {

    [SerializeField]
    private List<Performable> effectLists;
    public override void performSkill(List<PakRender> targets, PakRender self, Action onComplete) {
        if (targets.Count == 0)
            throw new Exception("Target is not provided");

        for (int i = 0; i < this.effectLists.Count; i++) {
            Vector3 targetPos = targets[0].GetPosition();
            self.Attack(targetPos, () => {
                // Callback of attack effect
                effectLists[i].Execute(targets, self);
            },
            onComplete
            );
        }
    }

}
