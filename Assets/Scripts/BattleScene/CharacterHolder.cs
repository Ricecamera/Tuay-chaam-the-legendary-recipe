using System;
using UnityEngine;


public class CharacterHolder
{
    static string LAYER_DEFAULT = "Default";
    static string LAYTER_FRONT = "Front";
    static float SIZE_MULTIPLER = 1.12f;

    public bool Selected {get; private set;}    // Is this character selected by player?
    public bool InAction {get; private set;}     // Is this character setted its action?

    public GameObject character;    // game object of this character

    // Constructors
    public CharacterHolder()
    {
        character = null;
        Selected = false;
        InAction = false;

    }

    public CharacterHolder(GameObject character)
    {
        this.character = character;
        Selected = false;
        InAction = false;

        // Hide Action Icon
        PakRender render = character.GetComponent<PakRender>();
        // render.DisplayInAction(false);
    }

    public void Select(bool value)
    {
        Selected = value;
        if (value)
        {
            character.GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
        }
        else
        {
            character.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        character.GetComponent<PakRender>().ShowSelected(value);
    }

    public void Action(bool value, int index)
    {
        InAction = value;
        PakRender render = character.GetComponent<PakRender>();
        if (value)
            render.DisplayInAction(true, index);
        else
            render.DisplayInAction(false, index);
    }

    public void HighLightLayer(bool isHighLight) {
        PakRender renderer = character.GetComponent<PakRender>();
        if (isHighLight) {
            renderer.GoToLayer(LAYTER_FRONT);
        }
        else {
            renderer.GoToLayer(LAYER_DEFAULT);
        }
    }
}
