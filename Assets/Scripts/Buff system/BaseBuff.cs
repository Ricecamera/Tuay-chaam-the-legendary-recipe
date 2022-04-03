using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffSystem {
    
    public abstract class BaseBuff : ScriptableObject
    {
        public string buffName;     // unique name of this buff, can use as a key

        public int duration;        // the number of turn this effect last

        public Sprite buffIcon;
    }
}