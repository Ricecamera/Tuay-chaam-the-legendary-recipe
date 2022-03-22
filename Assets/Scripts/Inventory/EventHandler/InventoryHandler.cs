using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ItemImage;
    public ItemType itemType;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop");
        if (eventData.pointerDrag != null)
        {
            //Debug.Log(eventData.pointerDrag.name);
            //Debug.Log(eventData.pointerDrag.transform.parent);

            // swap item กับ ช่องว่าง
            if (!ItemImage.activeSelf && itemType == eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.type &&
                (!ItemImage.transform.parent.CompareTag("inventory") || !eventData.pointerDrag.transform.parent.CompareTag("inventory")))
            {

                ImageHandler.SwapItem(eventData.pointerDrag.gameObject, ItemImage);

                ItemImage.SetActive(true);
                ItemImage.GetComponent<Image>().sprite = eventData.pointerDrag.gameObject.GetComponent<Image>().sprite;

                ItemImage.GetComponent<ImageHandler>().itemObject = eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject;

                eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject = null;

                eventData.pointerDrag.gameObject.GetComponent<Image>().sprite = null;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.transform.parent.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
                eventData.pointerDrag.gameObject.SetActive(false);
                Debug.Log("check swap");

                // if (itemType != ItemType.Chaam)
                // {
                //     UpdateCharacter(ItemImage);
                // }
                // else
                // {
                //     UpdateChaam(ItemImage);
                // }



            }

            // swap item กับ item
            // else
            // {
            //     Debug.Log("#1 : " + eventData.pointerDrag.gameObject.GetComponent<Image>().sprite + " / " + ItemImage.GetComponent<Image>().sprite);
            //     Sprite tempImage = eventData.pointerDrag.gameObject.GetComponent<Image>().sprite;
            //     eventData.pointerDrag.gameObject.GetComponent<Image>().sprite = ItemImage.GetComponent<Image>().sprite;
            //     ItemImage.GetComponent<Image>().sprite = tempImage;
            //     eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.transform.parent.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
            //     Debug.Log("#2 : " + eventData.pointerDrag.gameObject.GetComponent<Image>().sprite + " / " + ItemImage.GetComponent<Image>().sprite);
            // }


        }
    }

    public void UpdateCharacter(GameObject character)
    {
        CharacterSelecter.instance.AddCharacter(character.GetComponent<ImageHandler>().itemObject);
        // CharacterSelecter.instance.AddCharacterName(character.GetComponent<ImageHandler>().itemObject.name);
        //Debug.Log(CharacterSelecter.instance.GetCharacters().Count);
    }

    public void UpdateSupport(GameObject support)
    {
        CharacterSelecter.instance.AddCharacter(support.GetComponent<ImageHandler>().itemObject);
        // CharacterSelecter.instance.AddCharacterName(character.GetComponent<ImageHandler>().itemObject.name);
        //Debug.Log(CharacterSelecter.instance.GetCharacters().Count);
    }

    public void UpdateChaam(GameObject chaam)
    {
        CharacterSelecter.instance.SetChaam(chaam.GetComponent<ImageHandler>().itemObject);
        // CharacterSelecter.instance.AddCharacterName(character.GetComponent<ImageHandler>().itemObject.name);
    }
}
