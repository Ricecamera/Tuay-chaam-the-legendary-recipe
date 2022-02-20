using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryObject inventory;

    public int ORIGIN_POSITION_X;
    public int ORIGIN_POSITION_Y;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponent<RectTransform>().localScale = new Vector3(5f, 5f, 0f);
            itemsDisplayed.Add(inventory.Container[i], obj);
        }

    }


    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                if (i == 0) obj.GetComponent<RectTransform>().localScale = new Vector3(10f, 10f, 0f);
                else obj.GetComponent<RectTransform>().localScale = new Vector3(7f, 7f, 0f);
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(-24.54f, 42.61f, 0f);
                break;
            case 1:
                v = new Vector3(88.78959f, 92.34114f, 0f);
                break;
            case 2:
                v = new Vector3(88.79f, -7.9f, 0f);
                break;
            case 3:
                v = new Vector3(216.8f, 92.341f, 0f);
                break;
            case 4:
                v = new Vector3(216.8f, -7.9f, 0f);
                break;
        }

        return v;
    }
}
