using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
//ISerializationCallbackReceiver

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public Inventory Container;
    //public ItemDatabaseObject database;

    //public void AddItem(ItemObject item,int idx) 
    //{
    //    Container.MainItems[idx].UpdateSlot(item._name, item, 1);
    //}

    [ContextMenu("Clear")]
    public void Clear()
    {
        this.Container = new Inventory();
    }

    public void ResetInventory()
    {
        for (int i = 0; i < Container.MainItems.Length; i++)
        {
            this.Container.MainItems[i] = new InventorySlot();
        }
        for (int i = 0; i < Container.ChaamItems.Length; i++)
        {
            this.Container.ChaamItems[i] = new InventorySlot();
        }
        for (int i = 0; i < Container.SupportItems.Length; i++)
        {
            this.Container.SupportItems[i] = new InventorySlot();
        }
    }

}


[System.Serializable]

public class Inventory
{

    public InventorySlot[] MainItems = new InventorySlot[6];
    public InventorySlot[] ChaamItems = new InventorySlot[6];
    public InventorySlot[] SupportItems = new InventorySlot[6];

    public Inventory()
    {
        MainItems = new InventorySlot[6];
        ChaamItems = new InventorySlot[6];
        SupportItems = new InventorySlot[6];
    }
}



[System.Serializable]
public class InventorySlot
{
    public string Name;
    public ItemObject item;
    public int amount;
    public InventorySlot()
    {
        Name = "";
        item = null;
        amount = 0;
    }
    public InventorySlot(string _name, ItemObject _item, int _amount)
    {
        Name = _name;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(string _name, ItemObject _item, int _amount)
    {
        Name = _name;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
