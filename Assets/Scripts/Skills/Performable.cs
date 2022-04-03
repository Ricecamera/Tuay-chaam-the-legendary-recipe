using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Performable {

    public void Execute(List<PakRender> target, PakRender self);
}