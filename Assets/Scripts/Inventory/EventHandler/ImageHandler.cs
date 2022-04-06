using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler,
                            IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    private RectTransform rt;

    // [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private List<GameObject> mouseHover;

    public ItemObject itemObject;


    private void Start()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.transform.parent.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        //FetchSkillData();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Pointer Up");
        CharacterDetail.ChangeDetail(itemObject._name.ToUpper(), "Skill 1 : asdjwidjwda Skill 2 : asdhwuidhaw Skill 3 : cjoiajdowdwd Skill 4 : cjoiajdowdwd", itemObject.uiDisplay);

        // swap between item and item
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" && eventData.pointerCurrentRaycast.gameObject.activeSelf &&
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.type == eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.type) //TODO
        {

            SwapItem(eventData.pointerDrag.gameObject, eventData.pointerCurrentRaycast.gameObject);

            Sprite tempImage = eventData.pointerDrag.gameObject.GetComponent<Image>().sprite;
            //Debug.Log("#1" + eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            //Debug.Log("#1" + eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            ItemObject tempObject = itemObject;
            eventData.pointerDrag.gameObject.GetComponent<Image>().sprite = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;
            eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject = eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite = tempImage;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject = tempObject;
            //Debug.Log("#2" + eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            //Debug.Log("#2" + eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);


        }
    }

    public static void SwapItem(GameObject dragItem, GameObject currentItem)
    {
        if (dragItem.transform.parent.CompareTag("slot") && currentItem.transform.parent.CompareTag("inventory"))
        {
            if (dragItem.GetComponent<ImageHandler>().itemObject != null) CharacterSelecter.instance.RemoveCharacter(dragItem.GetComponent<ImageHandler>().itemObject);

            if (currentItem.GetComponent<ImageHandler>().itemObject != null)
            {
                CharacterSelecter.instance.AddCharacter(currentItem.GetComponent<ImageHandler>().itemObject);
            }
        }
        else if (currentItem.transform.parent.CompareTag("slot") && dragItem.transform.parent.CompareTag("inventory"))
        {
            if (currentItem.GetComponent<ImageHandler>().itemObject != null) CharacterSelecter.instance.RemoveCharacter(currentItem.GetComponent<ImageHandler>().itemObject);
            if (dragItem.GetComponent<ImageHandler>().itemObject != null)
            {
                CharacterSelecter.instance.AddCharacter(dragItem.GetComponent<ImageHandler>().itemObject);
            }
        }
    }

    public void FetchSkillData()
    {
        //Debug.Log(this.itemObject.prefab.gameObject.GetComponent<CarrotRender>());
    }
}
