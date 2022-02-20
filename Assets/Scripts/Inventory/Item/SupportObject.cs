using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Support Object", menuName = "Inventory System/Items/SupportItem")]
public class SupportObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Support;
        // Debug.Log(prefab.AddComponent<EggplantRender>().healthSystem.CurrentHp);
    }
}
