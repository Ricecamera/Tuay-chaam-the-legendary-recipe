using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pak Object", menuName = "Inventory System/Items/PakItem")]
public class PakObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Pak;
        // Debug.Log(prefab.AddComponent<EggplantRender>().healthSystem.CurrentHp);
    }
}
