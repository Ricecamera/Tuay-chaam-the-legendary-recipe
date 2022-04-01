using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffSystem.Behaviour {
    public interface ILastingBehaviour {
        public void OnAddBuff(PakRender character);

        public void OnRemoveBuff(PakRender character);
    }
}

