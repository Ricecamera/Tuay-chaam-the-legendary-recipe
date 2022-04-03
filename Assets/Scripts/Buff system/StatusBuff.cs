using System;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem.Behaviour;

namespace BuffSystem {

    [CreateAssetMenu(fileName = "New Status Buff", menuName = "Assets/Buff/status buff")]
    public class StatusBuff : BaseBuff, ILastingBehaviour {

        [Serializable]
        private class StatusPair {
            public string statusName;

            [Range(-1, 1)]
            public float ratio;
        }


        [SerializeField]
        private StatusPair[] statusArray;
        
        public void Initialize(PakRender character) {
            for (int i = 0; i < statusArray.Length ; i++) {
                UpdateStatus(statusArray[i].statusName, statusArray[i].ratio, character);
            }
        }

        public void Deactivate(PakRender character) {
            for (int i = 0; i < statusArray.Length; i++) {
                // Remove status buff from character by subtract the current value by the add amount
                UpdateStatus(statusArray[i].statusName, -1 * statusArray[i].ratio, character);
            }
        }

        public void UpdateStatus(string status, float ratio, PakRender character) {
            if (status.Equals("ATK")) {
                character.currentAtk += (int) ratio * character.currentAtk;
            }
            else if (status.Equals("DEF")) {
                character.currentDef += (int) ratio * character.currentDef;
            }
            else if (status.Equals("SPD")) {
                character.currentSpeed += (int) ratio * character.currentSpeed;
            }
        }
    }
}
