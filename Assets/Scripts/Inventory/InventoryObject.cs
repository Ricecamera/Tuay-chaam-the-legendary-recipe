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
    public string savePath;
    public Inventory Container;
    public ItemDatabaseObject database;
    // public void AddItem(ItemObject _item, int _amount)
    // {
    //     for (int i = 0; i < Container.Items.Length; i++)
    //     {
    //         if (Container.Items[i].ID == _item.id)
    //         {
    //             Container.Items[i].AddAmount(_amount);
    //             return;
    //         }
    //     }
    // }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    // [ContextMenu("Load")]
    // public void Load()
    // {

    //     if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
    //     {
    //         IFormatter formatter = new BinaryFormatter();
    //         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
    //         Inventory newContainer = (Inventory)formatter.Deserialize(stream);
    //         for (int i = 0; i < Container.Items.Length; i++)
    //         {
    //             Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
    //         }
    //         stream.Close();

    //     }

    // }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }

}


[System.Serializable]
// public class Inventory
// {
//     public InventorySlot[] Items = new InventorySlot[6];
// }

public class Inventory
{
    public InventorySlot[] MainItems = new InventorySlot[6];
    public InventorySlot[] ChaamItems = new InventorySlot[6];
    public InventorySlot[] SupportItems = new InventorySlot[6];
}



[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public ItemObject item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
