using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour
{
    [SerializeField]
    private GameObject starL;
    [SerializeField]
    private GameObject starR;
    [SerializeField]
    private GameObject starM;

    [SerializeField]
    private Canvas canvas;

    private List<ItemObject> itemList;

    [SerializeField]
    private RuntimeAnimatorController animator;
    //* Tester
    //* private int dieCount;

    private void Start()
    {
        Debug.Log(LevelManager.instance.thislevel);
        CharacterSelecter.instance.ResetCharacter();
        CharacterSelecter.instance.chaam = SaveManager.instance.playerDatabase.GetInventory().Container.ChaamItems[0].item;
        // TODO set stars to correct state 
        //* die = 0 -> 3 stars -> all star.GetComponent<Animator>().setTrigger("Start")
        //* die = 1 -> 2 stars -> starR is not trigger
        //* die = 2 -> 1 star -> starR and starM are not trigger
        //* die = 3 -> 0 stars -> no star is trigger

        if (SaveManager.instance.GetDieCount() == 0)
        //* Tester
        //* if (dieCount == 0)
        {
            starR.SetActive(true);
            starL.SetActive(true);
            starM.SetActive(true);
        }
        else if (SaveManager.instance.GetDieCount() == 1)
        //* Tester
        //* else if (dieCount == 1)
        {
            starL.SetActive(true);
            starM.SetActive(true);
        }
        else if (SaveManager.instance.GetDieCount() == 2)
        //* Tester
        //* else if (dieCount == 2)
        {
            starL.SetActive(true);
        }
        else if (SaveManager.instance.GetDieCount() == 3)
        //* Tester
        //* else if (dieCount == 3)
        {

        }

        //TODO drop item  
        // Debug.Log(LevelManager.instance.thislevel);
        itemList = MaptoItem.Instance.GetItemList(LevelManager.instance.thislevel);
        // itemList = MaptoItem.Instance.GetItemList(2);
        Debug.Log(itemList);
        int x = 0;
        int i = 0;
        //* Vector3(75,-126,0)
        foreach (ItemObject item in itemList)
        {
            Debug.Log(item._name);
            GameObject imageItem = new GameObject("ImageItem");
            imageItem.AddComponent<Image>();
            imageItem.AddComponent<Animator>();
            imageItem.GetComponent<Animator>().runtimeAnimatorController = animator;
            imageItem.GetComponent<Image>().sprite = item.uiDisplay;
            imageItem.transform.SetParent(canvas.transform);
            imageItem.transform.GetComponent<RectTransform>().localPosition = new Vector3(x + 75 * i, -120, 0);
            imageItem.transform.GetComponent<Image>().preserveAspect = true;
            Debug.Log("Test item" + item._name);


            bool addPak = false;
            foreach (var slot in DatabaseManager.instance.GetPlayerDatabase().GetInventory().Container.MainItems)
            {
                if (slot.item == item)
                {
                    addPak = true;
                    break;
                }
            }
            foreach (var slot in DatabaseManager.instance.GetPlayerDatabase().GetInventory().Container.ChaamItems)
            {
                if (addPak) break;
                if (slot.item == item)
                {
                    addPak = true;
                    break;
                }
            }
            foreach (var slot in DatabaseManager.instance.GetPlayerDatabase().GetInventory().Container.SupportItems)
            {
                if (addPak) break;
                if (slot.item == item)
                {
                    addPak = true;
                    break;
                }
            }

            if (!addPak) DatabaseManager.instance.AddItemToInventoryByName(item._name);
            i++;
        }


    }
}
