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

    //* Tester
    private int dieCount;

    private void Start()
    {
        // TODO set stars to correct state 
        //* die = 0 -> 3 stars -> all star.GetComponent<Animator>().setTrigger("Start")
        //* die = 1 -> 2 stars -> starR is not trigger
        //* die = 2 -> 1 star -> starR and starM are not trigger
        //* die = 3 -> 0 stars -> no star is trigger

        //* Tester
        // MaptoItem.instance.Awake();

        // if (BattleScene.BattleManager.instance.GetDieCount() == 0)
        if (dieCount == 0)
        {
            starR.SetActive(true);
            starL.SetActive(true);
            starM.SetActive(true);
        }
        // else if (BattleScene.BattleManager.instance.GetDieCount() == 1)
        else if (dieCount == 1)
        {
            starL.SetActive(true);
            starM.SetActive(true);
        }
        // else if (BattleScene.BattleManager.instance.GetDieCount() == 2)
        else if (dieCount == 2)
        {
            starL.SetActive(true);
        }
        // else if (BattleScene.BattleManager.instance.GetDieCount() == 3)
        else if (dieCount == 3)
        {

        }

        //TODO drop item  
        // Debug.Log(LevelManager.instance.thislevel);
        // itemList = MaptoItem.Instance.GetItemList(LevelManager.instance.thislevel);
        itemList = MaptoItem.Instance.GetItemList(3);
        Debug.Log(itemList);
        foreach (ItemObject item in itemList)
        {
            GameObject imageItem = new GameObject("ImageItem");
            imageItem.AddComponent<Image>();
            imageItem.GetComponent<Image>().sprite = item.uiDisplay;
            imageItem.transform.SetParent(canvas.transform);
            imageItem.transform.GetComponent<RectTransform>().localPosition = new Vector3(0, -120, 0);
            DatabaseManager.instance.AddItemToInventoryByName(item._name);
            //GameObject itemInstance = Instantiate(imageItem) as GameObject;
            // itemInstance.AddComponent<Image>();
            // itemInstance.GetComponent<image>().sprite = item.uiDisplay;
            //itemInstance.transform.position = new Vector3(0, 0, 0);
        }
    }
}
