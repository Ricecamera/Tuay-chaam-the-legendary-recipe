using System.Collections;
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
    public List<int> starInMap = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0};
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
        SaveData.current.starInMap = this.starInMap;
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
        this.starInMap = SaveData.current.starInMap;
    }

    public void setStar(int mapLevel, int star)
    {
        starInMap[mapLevel] = star;
    }

    public void resetStar()
    {
        starInMap = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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

    public List<int> starInMap;

    public void Clear()
    {
        unlockStatus = 1;
        cookSystemStatus = PlayerProgress.LOCKED;
        inventory = new List<string>();
        starInMap = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

}
