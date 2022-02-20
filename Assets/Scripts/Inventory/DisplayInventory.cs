using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject teamSlot;
    public GameObject InventoryPrefab1;
    public GameObject InventoryPrefab2;
    public GameObject InventoryPrefab3;
    public GameObject mainSlot;
    public GameObject chaamSlot;
    public GameObject supportSlot;

    static Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    static Dictionary<GameObject, InventorySlot> ChaamItems = new Dictionary<GameObject, InventorySlot>();
    static Dictionary<GameObject, InventorySlot> MainItems = new Dictionary<GameObject, InventorySlot>();
    static Dictionary<GameObject, InventorySlot> SupportItems = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots2();
    }

    void Update()
    {

    }

    // Initialize slot with item
    // public void CreateSlots()
    // {
    //     ChaamItems = new Dictionary<GameObject, InventorySlot>();
    //     for (int i = 0; i < inventory.Container.Items.Length; i++)
    //     {
    //         var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //         Debug.Log(inventory.Container.Items[i].item);
    //         if (inventory.Container.Items[i].item != null)
    //         {
    //             obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.Items[i].item.uiDisplay;
    //         }
    //         else
    //         {
    //             obj.transform.GetChild(1).gameObject.SetActive(false);
    //         }

    //         ChaamItems.Add(obj, inventory.Container.Items[i]);
    //     }
    // }

    // public void CreateSlots()
    // {
    //     ChaamItems = new Dictionary<GameObject, InventorySlot>();
    //     for (int i = 0; i < inventory.Container.ChaamItems.Length; i++)
    //     {
    //         var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //         // Debug.Log(inventory.Container.ChaamItems[i].item);
    //         if (inventory.Container.ChaamItems[i].item != null)
    //         {
    //             obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.ChaamItems[i].item.uiDisplay;
    //         }
    //         else
    //         {
    //             obj.transform.GetChild(1).gameObject.SetActive(false);
    //         }

    //         ChaamItems.Add(obj, inventory.Container.ChaamItems[i]);
    //     }

    //     // MainItems = new Dictionary<GameObject, InventorySlot>();
    //     // for (int i = 0; i < inventory.Container.MainItems.Length; i++)
    //     // {
    //     //     var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //     //     obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //     //     Debug.Log(inventory.Container.MainItems[i].item);
    //     //     if (inventory.Container.MainItems[i].item != null)
    //     //     {
    //     //         obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.MainItems[i].item.uiDisplay;
    //     //     }
    //     //     else
    //     //     {
    //     //         obj.transform.GetChild(1).gameObject.SetActive(false);
    //     //     }

    //     //     MainItems.Add(obj, inventory.Container.MainItems[i]);
    //     // }
    // }

    // เปลี่ยน tab



    public void CreateSlots2()
    {
        ChaamItems = new Dictionary<GameObject, InventorySlot>();
        MainItems = new Dictionary<GameObject, InventorySlot>();
        SupportItems = new Dictionary<GameObject, InventorySlot>();
        InitChaamItems();
        InitMainItems();
        InitSupportItems();
        InitTeamSlot();
    }

    private void InitTeamSlot()
    {
        for (int i = 0; i < teamSlot.Container.MainItems.Length; i++)
        {
            var obj = Instantiate(mainSlot, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPositionMain(i);
            //obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            //obj.transform.GetChild(0).GetComponent<RectTransform>().localPosition = GetPositionMain(i);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            obj.transform.GetChild(1).gameObject.SetActive(false);
            // ChaamItems.Add(obj, teamSlot.Container.MainItems[i]);
        }
        for (int i = 0; i < teamSlot.Container.ChaamItems.Length; i++)
        {
            var obj = Instantiate(chaamSlot, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            //obj.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(-14.10001f, 42.5f, 0f);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            obj.transform.GetChild(1).gameObject.SetActive(false);
            //ChaamItems.Add(obj, teamSlot.Container.MainItems[i]);
        }
        for (int i = 0; i < teamSlot.Container.SupportItems.Length; i++)
        {
            var obj = Instantiate(supportSlot, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = TestPosition(i);
            //obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            //obj.transform.GetChild(0).GetComponent<RectTransform>().localPosition = GetPositionSupport(i);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            obj.transform.GetChild(1).gameObject.SetActive(false);
            //ChaamItems.Add(obj, teamSlot.Container.MainItems[i]);
        }
    }

    public Vector3 TestPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(0f, 0f, 0f);
                break;
            case 1:
                v = new Vector3(0f, -60f, 0f);
                break;
            case 2:
                v = new Vector3(60f, 30f, 0f);
                break;
            case 3:
                v = new Vector3(60f, -30f, 0f);
                break;
            case 4:
                v = new Vector3(60f, -90f, 0f);
                break;
        }

        return v;
    }

    public Vector3 GetPositionSupport(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(-265f, 73.29999f, 0f);
                break;
            case 1:
                v = new Vector3(-265f, 17.3f, 0f);
                break;
            case 2:
                v = new Vector3(-202f, 98.29999f, 0f);
                break;
            case 3:
                v = new Vector3(-202f, 42.29999f, 0f);
                break;
            case 4:
                v = new Vector3(-202f, -15f, 0f);
                break;
        }

        return v;
    }

    public Vector3 GetPositionMain(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(0f, 0f, 0f);
                break;
            case 1:
                v = new Vector3(80f, 0f, 0f);
                break;
            case 2:
                v = new Vector3(0f, -90f, 0f);
                break;
            case 3:
                v = new Vector3(80f, -90f, 0f);
                break;
        }

        return v;
    }

    public void InitChaamItems()
    {
        for (int i = 0; i < inventory.Container.ChaamItems.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab1, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            if (inventory.Container.ChaamItems[i].item != null)
            {
                obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.ChaamItems[i].item.uiDisplay;

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = inventory.Container.ChaamItems[i].item;
            }
            else
            {
                obj.transform.GetChild(1).gameObject.SetActive(false);

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            }
            ChaamItems.Add(obj, inventory.Container.ChaamItems[i]);
        }
    }

    public void InitMainItems()
    {
        for (int i = 0; i < inventory.Container.MainItems.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab2, Vector3.zero, Quaternion.identity, transform);
            obj.SetActive(false);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            if (inventory.Container.MainItems[i].item != null)
            {
                obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.MainItems[i].item.uiDisplay;

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = inventory.Container.MainItems[i].item;
            }
            else
            {
                obj.transform.GetChild(1).gameObject.SetActive(false);

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;
            }
            //obj.transform.GetChild(1).gameObject.SetActive(false);
            MainItems.Add(obj, inventory.Container.MainItems[i]);
        }
    }
    public void InitSupportItems()
    {

        for (int i = 0; i < inventory.Container.SupportItems.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab3, Vector3.zero, Quaternion.identity, transform);
            obj.SetActive(false);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            // Debug.Log(inventory.Container.ChaamItems[i].item);
            if (inventory.Container.SupportItems[i].item != null)
            {
                obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inventory.Container.SupportItems[i].item.uiDisplay;

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = inventory.Container.SupportItems[i].item;
            }
            else
            {
                obj.transform.GetChild(1).gameObject.SetActive(false);

                //TODO
                obj.transform.GetChild(1).gameObject.GetComponent<ImageHandler>().itemObject = null;

            }
            //obj.transform.GetChild(1).gameObject.SetActive(false);
            SupportItems.Add(obj, inventory.Container.SupportItems[i]);
        }
    }


    public static void ChangeSlot3(int n)
    {
        switch (n)
        {
            case 1:
                {
                    foreach (var item in ChaamItems)
                    {
                        item.Key.transform.gameObject.SetActive(true);
                    }
                    foreach (var item in MainItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    foreach (var item in SupportItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    break;
                }
            case 2:
                {
                    foreach (var item in MainItems)
                    {
                        item.Key.transform.gameObject.SetActive(true);
                    }
                    foreach (var item in ChaamItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    foreach (var item in SupportItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    break;
                }
            case 3:
                {
                    foreach (var item in SupportItems)
                    {
                        item.Key.transform.gameObject.SetActive(true);
                    }
                    foreach (var item in ChaamItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    foreach (var item in MainItems)
                    {
                        item.Key.transform.gameObject.SetActive(false);
                    }
                    break;
                }
        }

    }


    // เมื่อกดปุ่มเพื่อดูของใน inventory เพิ่มเติม
    // public void UpdateSlot()
    // {
    //     foreach (KeyValuePair<GameObject,InventorySlot> _slot in itemsDisplayed)
    //     {
    //         if (_slot.Value.ID >= 0)
    //         {
    //             _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
    //             _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

    //         }
    //         else
    //         {
    //             _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
    //             _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
    //             _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         }
    //     }
    // }

    public Vector3 GetPosition(int i)
    {
        Vector3 v = new Vector3(0f, 0f, 0f); ;
        switch (i)
        {
            case 0:
                v = new Vector3(0f, 0f, 0f);
                break;
            case 1:
                v = new Vector3(80f, 0f, 0f);
                break;
            case 2:
                v = new Vector3(160f, 0f, 0f);
                break;
            case 3:
                v = new Vector3(240f, 0f, 0f);
                break;
            case 4:
                v = new Vector3(320f, 0f, 0f);
                break;
            case 5:
                v = new Vector3(400f, 0f, 0f);
                break;
        }

        return v;
    }
}
