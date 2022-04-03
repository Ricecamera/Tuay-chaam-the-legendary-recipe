using UnityEngine;
using BuffSystem.Behaviour;

namespace BuffSystem {

    [CreateAssetMenu(fileName = "New DOT Buff", menuName = "Assets/Buff/DOT buff")]
    public class DOTBuff : BaseBuff, IOvertimeBehaviour {

        [Range(0, 1)]
        public float damageRatio;

        public void OnChangeTurn(PakRender character) {
            int damage = (int) damageRatio * character.healthSystem.MaxHp;
            character.healthSystem.TakeDamage(damage);
        }

    }
}
