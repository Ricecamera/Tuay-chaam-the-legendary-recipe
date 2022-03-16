using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour
{
    public static CharacterSelecter instance;

    public GameObject popup;

    private List<ItemObject> characters = new List<ItemObject>();
    private ItemObject chaam;
    public List<ItemObject> GetCharacters()
    {
        return characters;
    }

    public ItemObject GetChaam()
    {
        return chaam;
    }
    public void AddCharacter(ItemObject itemObject)
    {
        if (itemObject.type == ItemType.Pak)
        {
            this.characters.Add(itemObject);
        }
        else if (itemObject.type == ItemType.Chaam)
        {
            chaam = itemObject;
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
    }

    public void ResetCharacter()
    {
        this.characters = new List<ItemObject>();
        this.chaam = null;
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
    }

    public void SetChaam(ItemObject chaam)
    {
        this.chaam = chaam;
    }

    public void SetCharacter(List<ItemObject> characters)
    {
        this.characters = characters;
    }

    public void SetPopup(GameObject popup)
    {
        this.popup = popup;
    }

    public void ShowPopup()
    {
        popup.SetActive(true);
    }
}
