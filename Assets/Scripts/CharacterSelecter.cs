using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelecter : MonoBehaviour
{
    public static CharacterSelecter instance;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            ResetCharacter();
        }
    }

    //public GameObject popup; 

    public List<ItemObject> characters = new List<ItemObject>();
    public ItemObject chaam;
    public List<ItemObject> supports = new List<ItemObject>();


    public List<ItemObject> GetCharacters()
    {
        return characters;
    }

    public List<ItemObject> GetSupports()
    {
        return supports;
    }

    public ItemObject GetChaam()
    {
        return chaam;
    }
    public void AddCharacter(ItemObject itemObject)
    {
        if (itemObject.type == ItemType.Pak)
        {
            Debug.Log("Add Pak");
            this.characters.Add(itemObject);
        }
        else if (itemObject.type == ItemType.Chaam)
        {
            Debug.Log("Add Chaam");
            chaam = itemObject;
            Debug.Log(chaam.name);
        }
        else if (itemObject.type == ItemType.Support)
        {
            Debug.Log("Add Support");
            this.supports.Add(itemObject);
        }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("assign chaam");
            chaam = SaveManager.instance.playerDatabase.inventory.Container.ChaamItems[0].item;
        }
        else
        {
            Debug.Log("not working");
        }
    }

    public void ResetCharacter()
    {
        this.characters = new List<ItemObject>();
        this.supports = new List<ItemObject>();
        this.chaam = new ItemObject();
        Debug.Log("Reset all characters");
    }

    public void RemoveCharacter(ItemObject itemObject)
    {
        if (itemObject.type == ItemType.Chaam && chaam == itemObject)
        {
            chaam = null;
        }
        else if (itemObject.type == ItemType.Pak && this.characters.Contains(itemObject))
        {
            this.characters.Remove(itemObject);
        }
        else if (itemObject.type == ItemType.Support & this.supports.Contains(itemObject))
        {
            this.supports.Remove(itemObject);
        }
    }

    public void SetChaam(ItemObject chaam)
    {
        this.chaam = chaam;
    }

    // public void SetCharacter(List<ItemObject> characters)
    // {
    //     this.characters = characters;
    // }

    // public void SetSupport(List<ItemObject> supports)
    // {
    //     this.supports = supports;
    // }

    // public void SetPopup(GameObject popup)
    // {
    //     this.popup = popup;
    // }

    public void ShowPopup()
    {
        GameObject popupUI = GameObject.Find("PopUpUI");
        popupUI.transform.GetChild(0).gameObject.SetActive(true);
    }
}
