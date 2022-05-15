using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject scrollPane;

    Ray ray;
    RaycastHit hit;
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            scrollPane.SetActive(true);
        });
        scrollPane.SetActive(false);
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
        }
    }
}
