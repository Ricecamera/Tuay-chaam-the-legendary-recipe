using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltiptrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipScreenSpaceUI.showTooltip_Static(content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipScreenSpaceUI.hideTooltip_Static();
    }

    public void setContent(string contentString)
    {
        this.content = contentString;
    }


}
