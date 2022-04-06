using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pak Object", menuName = "Inventory System/Items/PakItem")]
public class PakObject : ItemObject
{
    List<string> skillName;
    private void Awake()
    {
        type = ItemType.Pak;
        skillName = new List<string>();
        // Debug.Log(prefab.AddComponent<EggplantRender>().healthSystem.CurrentHp);
    }

    // private void OnValidate()
    // {
    //     if (prefab == null) return;
    //     PakRender pak = prefab?.GetComponent<PakRender>();
    //     if (pak != null)
    //     {
    //         _name = pak.name.ToLower();
    //         uiDisplay = pak.Entity.image;
    //         description = pak.Entity.Description;

    //         for (int i = 0; i < pak.skills.Length; i++)
    //         {
    //             skillName.Add(pak.skills[i].skillName);
    //         }
    //     }
    // }
}
