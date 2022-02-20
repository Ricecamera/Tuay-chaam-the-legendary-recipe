using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ISerializationCallbackReceiver

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    public List<PakObject> pakItems;
    public List<ChaamObject> chaamItems;
    public List<SupportObject> supportItems;
}
