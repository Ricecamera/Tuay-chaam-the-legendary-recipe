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
        Debug.Log(DatabaseManager.instance.GetItemFromGameDB("tonhom"));
        AddItem(2, DatabaseManager.instance.GetItemFromGameDB("tonhom"));
        AddItem(3, DatabaseManager.instance.GetItemFromGameDB("cheepha"));
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
