using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltiptrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string headerText;

    public string describeText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipScreenSpaceUI.showTooltip_Static(headerText, describeText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipScreenSpaceUI.hideTooltip_Static();
    }

    public void setContent(string headerText, string describeText)
    {
        this.headerText = headerText;
        this.describeText = describeText;
    }


}
