using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour
{
    public static CharacterSelecter instance;

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
    public void AddCharacter(ItemObject character)
    {
        this.characters.Add(character);
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

    public void SetChaam(ItemObject chaam)
    {
        this.chaam = chaam;
    }
}
