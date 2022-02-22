using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject popup;

    private void Awake()
    {
        closeButton.onClick.AddListener(HidePopup);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }
}
