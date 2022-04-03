using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InventoryUI : MonoBehaviour
{
    private InventoryObject inventory;
    public PlayerDatabase playerDatabase;
    public GameObject InventorySlots;


    static Dictionary<GameObject, InventorySlot> inventorySlots = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        inventory = playerDatabase.GetInventory();
        CreateSlots();
    }


    public void CreateSlots()
    {
        InitInventorySlots();
        //ShowInventorySlots();
    }

    public void ShowInventorySlots()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
        gameObject.transform.GetChild(1).gameObject.SetActive(!gameObject.transform.GetChild(1).gameObject.activeSelf);
        gameObject.transform.GetChild(2).gameObject.SetActive(!gameObject.transform.GetChild(2).gameObject.activeSelf);
        foreach (var item in inventorySlots)
        {
            if (item.Key == null)
            {
                Debug.Log("Item is null");
            }
            else
            {
                item.Key.transform.gameObject.SetActive(!item.Key.transform.gameObject.activeSelf);
            }
        }
    }

    public void InitInventorySlots()
    {

        InitSlot(inventory.Container.MainItems, GetMainPosition);
        InitSlot(inventory.Container.ChaamItems, GetChaamPosition);
        InitSlot(inventory.Container.SupportItems, GetSupportPosition);
    }

    private void InitSlot(InventorySlot[] invSlot, Func<int, Vector3> position)
    {
        for (int i = 0; i < invSlot.Length; i++)
        {
            var obj = Instantiate(InventorySlots, Vector3.zero, Quaternion.identity, transform);
            obj.SetActive(false);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            obj.transform.GetChild(0).GetComponent<RectTransform>().localPosition = position(i);
            obj.transform.GetChild(1).GetComponent<RectTransform>().localPosition = position(i);
            if (invSlot[i].item != null)
            {
                obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = invSlot[i].item.uiDisplay;
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = invSlot[i].item;
            }
            else
            {
                obj.transform.GetChild(1).gameObject.SetActive(false);
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            }
            //obj.transform.GetChild(1).gameObject.SetActive(false);
            inventorySlots.Add(obj, invSlot[i]);
        }
    }

    public Vector3 GetMainPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(-655f, 250f, 0f);
                break;
            case 1:
                v = new Vector3(-475f, 250f, 0f);
                break;
            case 2:
                v = new Vector3(-295f, 250f, 0f);
                break;
            case 3:
                v = new Vector3(-115f, 250f, 0f);
                break;
            case 4:
                v = new Vector3(65f, 250f, 0f);
                break;
            case 5:
                v = new Vector3(245f, 250f, 0f);
                break;
        }

        return v;
    }

    public Vector3 GetChaamPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(-655f, 80f, 0f);
                break;
            case 1:
                v = new Vector3(-475f, 80f, 0f);
                break;
            case 2:
                v = new Vector3(-295f, 80f, 0f);
                break;
            case 3:
                v = new Vector3(-115f, 80f, 0f);
                break;
            case 4:
                v = new Vector3(65f, 80f, 0f);
                break;
            case 5:
                v = new Vector3(245f, 80f, 0f);
                break;
        }

        return v;
    }

    public Vector3 GetSupportPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(-655f, -90f, 0f);
                break;
            case 1:
                v = new Vector3(-475f, -90f, 0f);
                break;
            case 2:
                v = new Vector3(-295f, -90f, 0f);
                break;
            case 3:
                v = new Vector3(-115f, -90f, 0f);
                break;
            case 4:
                v = new Vector3(65f, -90f, 0f);
                break;
            case 5:
                v = new Vector3(245f, -90f, 0f);
                break;
        }

        return v;
    }
}
