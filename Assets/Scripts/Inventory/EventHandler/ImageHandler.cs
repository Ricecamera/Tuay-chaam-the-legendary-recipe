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
        //Debug.Log("Click point");
        transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.transform.parent.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // if (eventData.pointerDrag == null)
        // {
        eventData.pointerEnter.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");


        // swap between item and item
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" && eventData.pointerCurrentRaycast.gameObject.activeSelf &&
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.type == eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.type) //TODO
        {

            Sprite tempImage = eventData.pointerDrag.gameObject.GetComponent<Image>().sprite;
            Debug.Log("#1" + eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            Debug.Log("#1" + eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            ItemObject tempObject = itemObject;
            eventData.pointerDrag.gameObject.GetComponent<Image>().sprite = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;
            eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject = eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite = tempImage;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject = tempObject;
            Debug.Log("#2" + eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
            Debug.Log("#2" + eventData.pointerDrag.gameObject.GetComponent<ImageHandler>().itemObject.uiDisplay);
        }
    }
}
