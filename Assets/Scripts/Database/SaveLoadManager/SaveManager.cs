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
            GameObject loadbutton = GameObject.Find("Load Game");
            loadbutton.GetComponent<Button>().enabled = false;
            Debug.Log(loadbutton);
        }
    }

    public void InitData()
    {
        playerDatabase.inventory.ResetInventory();
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

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_data/player.txt");

        playerDatabase.PlayerDatabaseToSaveData();

        var json = JsonUtility.ToJson(SaveData.current);
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
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), SaveData.current);
            file.Close();

            playerDatabase.AddSaveDataToInventory();

            SceneManager.LoadScene("ModeSelection");
        }
    }

    public void NewGame()
    {
        InitData();
        playerDatabase.unlockStatus = 1;
        playerDatabase.resetStar();
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

