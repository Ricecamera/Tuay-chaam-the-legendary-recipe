using UnityEngine;

namespace BuffSystem.Behaviour {

    [CreateAssetMenu(fileName = "New Fix DOT Buff", menuName = "Assets/Buff/Fix DOT buff")]
    public class FixDOTBuff : BaseBuff, IOvertimeBehaviour {

        public int damage;
        public void OnChangeTurn(PakRender character) {
            character.healthSystem.TakeDamage(damage);
        }
    }
}
