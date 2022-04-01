using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    // Store all items
    public GameDatabase gameDatabase;

    // Store player's data
    public PlayerDatabase playerDatabase;

    public static DatabaseManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemObject GetItemFromGameDB(string name)
    {
        return gameDatabase.items.FirstOrDefault(item => item._name == name);
    }

    // Add item to databases when victory
    public void AddItemToInventory(ItemObject item)
    {
        switch (item.type)
        {
            case ItemType.Pak:
                var emptySlotPak = playerDatabase.GetInventory().Container.MainItems.FirstOrDefault(inventorySlot => inventorySlot.item == null);
                emptySlotPak.UpdateSlot(item._name, item, 1);
                break;
            case ItemType.Chaam:
                var emptySlotChaam = playerDatabase.GetInventory().Container.ChaamItems.FirstOrDefault(inventorySlot => inventorySlot.item == null);
                emptySlotChaam.UpdateSlot(item._name, item, 1);
                break;
            case ItemType.Support:
                var emptySlotSupport = playerDatabase.GetInventory().Container.SupportItems.FirstOrDefault(inventorySlot => inventorySlot.item == null);
                emptySlotSupport.UpdateSlot(item._name, item, 1);
                break;
        }
    }

    public void AddItemToInventoryByName(string name)
    {
        ItemObject item = GetItemFromGameDB(name);
        AddItemToInventory(item);
    }

    [ContextMenu("Add Item")]
    public void TestAddItem()
    {
        DatabaseManager.instance.AddItemToInventoryByName("garlic");
    }



    // public static PakObject GetPakByName(string _name)
    // {
    //     return instance.itemDatabase.pakItems.FirstOrDefault(x => x._name == _name);
    // }
    // public static ChaamObject GetChaamByName(string _name)
    // {
    //     return instance.itemDatabase.chaamItems.FirstOrDefault(x => x._name == _name);
    // }

    // public static SupportObject GetSupportByName(string _name)
    // {
    //     return instance.itemDatabase.supportItems.FirstOrDefault(x => x._name == _name);
    // }


}
