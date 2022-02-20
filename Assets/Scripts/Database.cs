using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour
{
    public ItemDatabaseObject pakDatabase;
    public ItemDatabaseObject chaamDatabase;
    private static Database instance;

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

    public static PakObject GetPakById(string ID)
    {
        return instance.pakDatabase.pakItems.FirstOrDefault(x => x.id == ID);
    }
    public static ChaamObject GetChaamById(string ID)
    {
        return instance.chaamDatabase.chaamItems.FirstOrDefault(x => x.id == ID);
    }

}
