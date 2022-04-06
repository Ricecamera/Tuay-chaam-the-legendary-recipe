using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/PlayerDatabase")]
public class PlayerDatabase : ScriptableObject
{
    public List<bool> unlockStatus { get; set; }
    public List<bool> playAniAlreadyMap { get; set; } 
    public InventoryObject inventoryObject;

    public InventoryObject GetInventory()
    {
        return this.inventoryObject;
    }
    public void SetInventory(InventoryObject inventoryObject)
    {
        this.inventoryObject = inventoryObject;
    }

}
