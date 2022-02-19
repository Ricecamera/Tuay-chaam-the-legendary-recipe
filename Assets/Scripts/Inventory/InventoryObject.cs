using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public ItemDatabaseObject database;
    public void AddItem(ItemObject _item, int _amount)
    {
        foreach (InventorySlot inv in Container)
        {
            if (inv.item == _item)
            {
                inv.AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }

    // JSON Util : Scriptable Obj. -> string
    // Binary Format, File Stream: Create file -> write string -> save path
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {

        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

}


[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public int ID;
    public InventorySlot(int _id, ItemObject _item, int _amount)
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
