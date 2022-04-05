using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/PlayerDatabase")]
public class PlayerDatabase : ScriptableObject
{
    public int mapUnlock;
    public InventoryObject inventoryObject;

    public InventoryObject GetInventory()
    {
        return this.inventoryObject;
    }
    public void SetInventory(InventoryObject inventoryObject)
    {
        this.inventoryObject = inventoryObject;
    }

    public int GetMapUnlock()
    {
        return this.mapUnlock;
    }

    public void SetMapUnlock(int n)
    {
        this.mapUnlock = n;
    }

}
