using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    //public List<PakObject> pakItems;
    //public List<ChaamObject> chaamItems;
    //public List<SupportObject> supportItems;
    public PakObject[] pakItems;
    public ChaamObject[] chaamItems;
    public SupportObject[] supportItems;
    //public ItemObject[] Items;
    //public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public ItemObject GetItem(string name)
    {
        return pakItems.FirstOrDefault(x => x._name == name);
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        pakItems = new PakObject[3];
        chaamItems = new ChaamObject[3];
        supportItems = new SupportObject[3];
    }
}
