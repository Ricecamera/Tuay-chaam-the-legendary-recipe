using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<GameObject> itemList;
    public Transform[] item;
    //TODO เราจะใช้ database ในการแสดงผล แต่ mock ตอนนี้จะใช้เป็น static
    void Start()
    {
        //TODO อ้างอิงตัวละครจากใน database ได้เลย initiate list จาก database นั้น
        //TODO ทำเป็นแบบร่างไว้ก่อน เอา script ไปติดกับ item แล้วเรียกใน SwitchTab
        itemList = new List<GameObject>();
        item = gameObject.GetComponentsInChildren<Transform>();
        foreach (var i in item)
        {
            itemList.Add(i.gameObject);
        }
    }

    public static List<GameObject> GetItemList()
    {
        return itemList;
    }

    public static void SetItemList(List<GameObject> item)
    {
        //TODO ตั้งใจให้ database มาเรียกเพื่อเซ็ตตัวละคร
        itemList = item;
    }
}
