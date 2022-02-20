using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipScreenSpaceUI : MonoBehaviour
{


    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;

    public static TooltipScreenSpaceUI instance { get; private set; }

    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("backgroundTooltip").GetComponent<RectTransform>();
        textMeshPro = transform.Find("textTooltip").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        hideTooltip();

    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            //Tooltip left screen on right side
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;

        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            //Tooltip left screen on top side
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;

        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void setText(string tooltipText)
    {


        textMeshPro.SetText(tooltipText);

        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(20, 8);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void showTooltip(string tooltipText)
    {
        gameObject.SetActive(true);
        setText(tooltipText);
    }

    private void hideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void showTooltip_Static(string tooltipString)
    {
        instance.showTooltip(tooltipString);
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
