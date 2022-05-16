// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// [CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/PlayerDatabase")]
// public class PlayerDatabase : ScriptableObject
// {
//     public int unlockStatus;
//     public InventoryObject inventoryObject;

//     // private void Start() {
//     //     unlockStatus = 1;
//     // }

//     public InventoryObject GetInventory()
//     {
//         return this.inventoryObject;
//     }
//     public void SetInventory(InventoryObject inventoryObject)
//     {
//         this.inventoryObject = inventoryObject;
//     }

// }

using System.Collections.Generic;
using UnityEngine;

public enum PlayerProgress
{
    LOCKED,
    ACQUIRED,
    UNLOCKED,
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/PlayerDatabase")]
public class PlayerDatabase : ScriptableObject
{
    public int unlockStatus;
    public PlayerProgress cookSystemStatus;
    public InventoryObject inventory;

    public void PlayerDatabaseToSaveData()
    {
        Debug.Log(SaveData.current);
        SaveData.current.Clear();
        foreach (InventorySlot inv in inventory.Container.MainItems) { if (inv.item != null) SaveData.current.inventory.Add(inv.Name); }
        foreach (InventorySlot inv in inventory.Container.ChaamItems) { if (inv.item != null) SaveData.current.inventory.Add(inv.Name); }
        foreach (InventorySlot inv in inventory.Container.SupportItems) { if (inv.item != null) SaveData.current.inventory.Add(inv.Name); }
        SaveData.current.unlockStatus = this.unlockStatus;
        SaveData.current.cookSystemStatus = this.cookSystemStatus;

    }

    public void AddSaveDataToInventory()
    {
        inventory.ResetInventory();
        foreach (string itemName in SaveData.current.inventory)
        {
            ItemObject item = DatabaseManager.instance.GetItemFromGameDB(itemName);
            DatabaseManager.instance.AddItemToInventory(item);
        }
        this.unlockStatus = SaveData.current.unlockStatus;
        this.cookSystemStatus = SaveData.current.cookSystemStatus;
    }

}

[System.Serializable]
public class SaveData
{
    public static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
    }

    public int unlockStatus;
    public List<string> inventory;
    public PlayerProgress cookSystemStatus;

    public void Clear()
    {
        unlockStatus = 1;
        cookSystemStatus = PlayerProgress.LOCKED;
        inventory = new List<string>();
    }

}
