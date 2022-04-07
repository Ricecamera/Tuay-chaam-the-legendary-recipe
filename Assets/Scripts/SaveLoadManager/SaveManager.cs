using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        if (!File.Exists(Application.persistentDataPath + "/game_save/player_data/player.txt"))
        {
            GameObject loadButton = GameObject.Find("Load Game");
            Debug.Log(loadButton);
            loadButton.GetComponent<Button>().interactable = false;
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

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_data"))
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

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_data");
        }

        if (File.Exists(Application.persistentDataPath + "/game_save/player_data/player.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_data/player.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), savedDatabase);
            file.Close();

            ConvertToPlayerDatabase(playerDatabase, savedDatabase);

            SceneManager.LoadScene("ModeSelection");
        }
    }

    public void NewGame()
    {
        InitData();
        SaveManager.instance.playerDatabase.unlockStatus = 1;
        if (File.Exists(Application.persistentDataPath + "/game_save/player_data/player.txt"))
        {
            Debug.Log("It's working");
            File.Delete(Application.persistentDataPath + "/game_save/player_data/player.txt");
        }

    }

    public void LoadGame()
    {
        Load();
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
        savedDatabase.unlockStatus = playerDatabase.unlockStatus;
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
        playerDatabase.unlockStatus = savedDatabase.unlockStatus;
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

}
