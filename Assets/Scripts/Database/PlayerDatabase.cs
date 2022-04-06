using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/PlayerDatabase")]
public class PlayerDatabase : ScriptableObject
{
    public int unlockStatus;
    public InventoryObject inventoryObject;

    // private void Start() {
    //     unlockStatus = 1;
    // }

    public InventoryObject GetInventory()
    {
        return this.inventoryObject;
    }
    public void SetInventory(InventoryObject inventoryObject)
    {
        this.inventoryObject = inventoryObject;
    }

}
