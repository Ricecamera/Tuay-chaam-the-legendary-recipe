using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Performable {
    public void performSkill(List<PakRender> target, PakRender self);
}