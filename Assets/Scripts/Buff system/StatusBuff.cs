using System;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem.Behaviour;

namespace BuffSystem {

    [Serializable]
    public class StatusPair {
        public StatLabel statusLabel;

        [Range(-1, 1)]
        public float ratio;
    }

    public enum StatLabel { ATK, DEF, SPD }

    [CreateAssetMenu(fileName = "New Status Buff", menuName = "Assets/Buff/status buff")]
    public class StatusBuff : BaseBuff, ILastingBehaviour {

        [SerializeField]
        private StatusPair[] statusArray;
        
        public void Initialize(PakRender character) {
            for (int i = 0; i < statusArray.Length ; i++) {
                UpdateStatus(statusArray[i].statusLabel, statusArray[i].ratio, character);
            }
        }

        public void Deactivate(PakRender character) {
            for (int i = 0; i < statusArray.Length; i++) {
                // Remove status buff from character by subtract the current value by the add amount
                UpdateStatus(statusArray[i].statusLabel, -1 * statusArray[i].ratio, character);
            }
        }

        public void UpdateStatus(StatLabel status, float ratio, PakRender character) {
            if (status == StatLabel.ATK) {
                character.BonusAtk += (int) ratio * character.baseAtk;
            }
            else if (status == StatLabel.DEF) {
                character.BonusDef += (int) ratio * character.baseDef;
            }
            else if (status == StatLabel.SPD) {
                character.BonusSpeed += (int) ratio * character.baseSpeed;
            }
        }
    }
}
