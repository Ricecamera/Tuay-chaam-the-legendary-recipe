using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaptoItem : MonoBehaviour
{
    public static MaptoItem instance;
    private Dictionary<int, List<ItemObject>> itemDictionary = new Dictionary<int, List<ItemObject>>();

    public static MaptoItem Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("MapToItem");
                go.AddComponent<MaptoItem>();
                go.GetComponent<MaptoItem>().SetItemMap();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    public void AddItem(int key, ItemObject item)
    {
        if (itemDictionary.ContainsKey(key))
        {
            itemDictionary[key].Add(item);
        }
        else
        {
            itemDictionary.Add(key, new List<ItemObject>() { item });
        }
    }

    public void SetItemMap()
    {
        // Debug.Log(DatabaseManager.instance.GetItemFromGameDB("cheepha"));
        AddItem(0, DatabaseManager.instance.GetItemFromGameDB("tonhom"));
        AddItem(1, DatabaseManager.instance.GetItemFromGameDB("carrot"));
        AddItem(1, DatabaseManager.instance.GetItemFromGameDB("salt"));
        AddItem(2, DatabaseManager.instance.GetItemFromGameDB("eggplant"));
        AddItem(2, DatabaseManager.instance.GetItemFromGameDB("honey"));
        AddItem(3, DatabaseManager.instance.GetItemFromGameDB("lime"));
        AddItem(3, DatabaseManager.instance.GetItemFromGameDB("garlic"));
        AddItem(4, DatabaseManager.instance.GetItemFromGameDB("chaam2"));
        AddItem(5, DatabaseManager.instance.GetItemFromGameDB("prikthai_support")); //support
        AddItem(6, DatabaseManager.instance.GetItemFromGameDB("brogli"));
        AddItem(7, DatabaseManager.instance.GetItemFromGameDB("normalprik_support")); //support
        AddItem(7, DatabaseManager.instance.GetItemFromGameDB("chaam1"));
        AddItem(8, DatabaseManager.instance.GetItemFromGameDB("cheepha"));
    }

    public List<ItemObject> GetItemList(int key)
    {
        if (itemDictionary.ContainsKey(key))
        {
            return itemDictionary[key];
        }
        else
        {
            return null;
        }
    }
}
