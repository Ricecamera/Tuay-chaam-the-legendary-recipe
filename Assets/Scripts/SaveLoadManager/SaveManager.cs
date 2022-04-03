using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public PlayerDatabase playerDatabase;

    public PlayerDatabase savedDatabase;

    public InventoryObject defaultInventory;

    private int dieCount;

    void Awake()
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

    public void InitData()
    {
        playerDatabase.inventoryObject.ResetInventory();
        foreach (InventorySlot inv in defaultInventory.Container.MainItems) { if (inv.item != null) DatabaseManager.instance.AddItemToInventory(inv.item); }
        foreach (InventorySlot inv in defaultInventory.Container.ChaamItems) { if (inv.item != null) DatabaseManager.instance.AddItemToInventory(inv.item); }
        foreach (InventorySlot inv in defaultInventory.Container.SupportItems) { if (inv.item != null) DatabaseManager.instance.AddItemToInventory(inv.item); }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    [ContextMenu("Save")]
    public void Save()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_data");
        }

        // Inventory => Database => Save
        //ConvertInventorytoDatabase(inventoryObject, playerDatabase);
        ConvertToSaveDatabase(playerDatabase, savedDatabase);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_data/player.txt");
        var json = JsonUtility.ToJson(savedDatabase);
        bf.Serialize(file, json);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {

        // PlayerDatabase obj = ScriptableObject.CreateInstance<PlayerDatabase>();
        // InventoryObject obj2 = ScriptableObject.CreateInstance<InventoryObject>();
        // obj.SetInventory(obj2);

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_data");
        }

        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_save/player_data/player.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_data/player.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), savedDatabase);
            file.Close();
        }

        ConvertToPlayerDatabase(playerDatabase, savedDatabase);

        // Load => Database => Inventory
        //ConvertDatabasetoInventory(inventoryObject, playerDatabase);
        //playerDatabase = obj;
    }

    public void NewGame()
    {
        InitData();
    }

    public void LoadGame()
    {
        Load();
        SceneManager.LoadScene("ModeSelection");
        //SceneLoader.Instance.LoadSceneByName("ModeSelection");
    }

    public void ConvertToSaveDatabase(PlayerDatabase playerDatabase, PlayerDatabase savedDatabase)
    {
        savedDatabase.GetInventory().ResetInventory();
        Debug.Log(savedDatabase.GetInventory());
        Debug.Log(savedDatabase.GetInventory().Container.MainItems[0].item);
        for (int i = 0; i < playerDatabase.GetInventory().Container.MainItems.Length; i++)
        {
            if (playerDatabase.GetInventory().Container.MainItems[i].item != null)
            {
                var slot = playerDatabase.GetInventory().Container.MainItems[i];
                savedDatabase.GetInventory().Container.MainItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        for (int i = 0; i < playerDatabase.GetInventory().Container.ChaamItems.Length; i++)
        {
            if (playerDatabase.GetInventory().Container.ChaamItems[i].item != null)
            {
                var slot = playerDatabase.GetInventory().Container.ChaamItems[i];
                savedDatabase.GetInventory().Container.ChaamItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        for (int i = 0; i < playerDatabase.GetInventory().Container.SupportItems.Length; i++)
        {
            if (playerDatabase.GetInventory().Container.SupportItems[i].item != null)
            {
                var slot = playerDatabase.GetInventory().Container.SupportItems[i];
                if (slot != null) savedDatabase.GetInventory().Container.SupportItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        savedDatabase.SetMapUnlock(playerDatabase.GetMapUnlock());
    }

    public void ConvertToPlayerDatabase(PlayerDatabase playerDatabase, PlayerDatabase savedDatabase)
    {
        playerDatabase.GetInventory().ResetInventory();
        for (int i = 0; i < savedDatabase.GetInventory().Container.MainItems.Length; i++)
        {
            if (savedDatabase.GetInventory().Container.MainItems[i].item != null)
            {
                var slot = savedDatabase.GetInventory().Container.MainItems[i];
                playerDatabase.GetInventory().Container.MainItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        for (int i = 0; i < savedDatabase.GetInventory().Container.ChaamItems.Length; i++)
        {
            if (savedDatabase.GetInventory().Container.ChaamItems[i].item != null)
            {
                var slot = savedDatabase.GetInventory().Container.ChaamItems[i];
                playerDatabase.GetInventory().Container.ChaamItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        for (int i = 0; i < savedDatabase.GetInventory().Container.SupportItems.Length; i++)
        {
            if (savedDatabase.GetInventory().Container.SupportItems[i].item != null)
            {
                var slot = savedDatabase.GetInventory().Container.SupportItems[i];
                playerDatabase.GetInventory().Container.SupportItems[i].UpdateSlot(slot.item._name, slot.item, 1);
            }
        }
        playerDatabase.SetMapUnlock(savedDatabase.GetMapUnlock());
    }

    public int GetDieCount()
    {
        return dieCount;
    }

    public void AddDieCount()
    {
        dieCount++;
    }

    public void SetZeroDieCount()
    {
        this.dieCount = 0;
    }

    // public void ConvertInventorytoDatabase(InventoryObject inv, PlayerDatabase db)
    // {
    //     for (int i = 0; i < 3; i++)
    //     {
    //     db.GetInventory().pakItems[i] = (PakObject)inv.Container.MainItems[i].item;
    //     db.GetInventory().chaamItems[i] = (ChaamObject)inv.Container.ChaamItems[i].item;
    //     db.GetInventory().supportItems[i] = (SupportObject)inv.Container.SupportItems[i].item;
    //     }
    // }

    // public void ConvertDatabasetoInventory(InventoryObject inv, PlayerDatabase db)
    // {
    //     for (int i = 0; i <db.GetInventory().pakItems.Length; i++)
    //     {
    //         if db.GetInventory().pakItems[i])
    //         {
    //             inv.Container.MainItems[i].UpdateSlotdb.GetInventory().pakItems[i]._name,db.GetInventory().pakItems[i], 1);
    //         }
    //         if db.GetInventory().chaamItems[i])
    //         {
    //             inv.Container.ChaamItems[i].UpdateSlotdb.GetInventory().chaamItems[i]._name,db.GetInventory().chaamItems[i], 1);
    //         }
    //         if db.GetInventory().supportItems[i])
    //         {
    //             inv.Container.SupportItems[i].UpdateSlotdb.GetInventory().supportItems[i]._name,db.GetInventory().supportItems[i], 1);
    //         }


    //     }
    // }

}
