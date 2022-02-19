using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaam Object", menuName = "Inventory System/Items/ChaamItem")]
public class ChaamObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Chaam;
        // Debug.Log(prefab.AddComponent<EggplantRender>().healthSystem.CurrentHp);
    }
}
