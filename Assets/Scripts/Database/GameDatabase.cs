using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Item Game Database", menuName = "Database/GameDatabase")]
public class GameDatabase : ScriptableObject
{
    public List<ItemObject> items;

}
