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

    public static bool chooseFlag; 
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            scrollPane.SetActive(true);
            chooseFlag = false;
        });
        scrollPane.SetActive(false);
        chooseFlag = true; 
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Target name: " + hit.collider.name);
        }
    }

}
