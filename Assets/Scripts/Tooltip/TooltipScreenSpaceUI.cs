using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipScreenSpaceUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI header;
    //private RectTransform backgroundRectTransform;

    [SerializeField]
    private TextMeshProUGUI description;

    public static TooltipScreenSpaceUI instance { get; private set; }

    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform canvasRectTransform;

    [SerializeField]
    private int characterLimit;

    private LayoutElement layoutElement;

    private void Awake()
    {
        instance = this;
        //backgroundRectTransform = transform.Find("backgroundTooltip").GetComponent<RectTransform>();
        //textMeshPro = transform.Find("textTooltip").GetComponent<TextMeshProUGUI>();
        // description = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        layoutElement = transform.GetComponent<LayoutElement>();

        hideTooltip();

    }

    private void Update()
    {
        //Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        // if (anchoredPosition.x + gameObject.GetComponent<Image>().sprite.rect.width > canvasRectTransform.rect.width)
        // {
        //     //Tooltip left screen on right side
        //     anchoredPosition.x = canvasRectTransform.rect.width - gameObject.GetComponent<Image>().sprite.rect.width;

        // }

        // if (anchoredPosition.y + gameObject.GetComponent<Image>().sprite.rect.height > canvasRectTransform.rect.height)
        // {
        //     //Tooltip left screen on top side
        //     anchoredPosition.y = canvasRectTransform.rect.height - gameObject.GetComponent<Image>().sprite.rect.height;

        // }

        // Debug.Log("Width : " + gameObject.GetComponent<Image>().sprite.rect.width + " Scale : " + rectTransform.localScale.x);
        // Debug.Log("Height : " + gameObject.GetComponent<Image>().sprite.rect.width + " Scale : " + rectTransform.localScale.x);

        // anchoredPosition.x = (canvasRectTransform.rect.width - gameObject.GetComponent<Image>().sprite.rect.width) * rectTransform.localScale.x;
        // anchoredPosition.y = (canvasRectTransform.rect.height - gameObject.GetComponent<Image>().sprite.rect.height) * rectTransform.localScale.y;
        // rectTransform.anchoredPosition = anchoredPosition;
        // gameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        Vector2 position = Input.mousePosition;

        transform.position = new Vector2(position.x + 50, position.y + 10);

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        int textLength = description.text.Length;

        layoutElement.enabled = (textLength > characterLimit) ? true : false;

    }

    private void setText(string headerText, string describeText)
    {

        header.SetText(headerText);
        header.ForceMeshUpdate();

        description.SetText(describeText);
        description.ForceMeshUpdate();

        // Vector2 textSize = textMeshPro.GetRenderedValues(false);
        // Vector2 paddingSize = new Vector2(20, 8);
        // backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void showTooltip(string headerText, string describeText)
    {
        gameObject.SetActive(true);
        setText(headerText, describeText);
    }

    private void hideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void showTooltip_Static(string headerText, string describeText)
    {
        instance.showTooltip(headerText, describeText);
    }

    public static void hideTooltip_Static()
    {
        instance.hideTooltip();
    }

    // public static void setTooltip_Static(string tooltipText)
    // {
    //     instance.setText(tooltipText);
    // }
}
