using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffSystem {
    
    public abstract class BaseBuff : ScriptableObject
    {
        public string buffName;     // unique name of this buff, can use as a key

        public string description;  // description of the effect of this buff

        public int duration;        // the number of turn this effect last

        public Sprite buffIcon;
    }
}