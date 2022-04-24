using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Pak,
    Chaam,
    Support
}
public class ItemObject : ScriptableObject
{
    public string _name;
    public Sprite uiDisplay;
    public ItemType type;
    public GameObject prefab;

    [TextArea(15, 20)]
    public string description;
}
