using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] popUps;
    private int popUpIndex;

    private Collider2D chaam;

    [SerializeField]
    private GameObject skillPanel;

    void Start()
    {
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0 && Input.GetMouseButtonDown(0))
        {
            clickChaam();
        }

    }

    public void clickChaam()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.GetComponent<PakRender>().Entity.EntityName == "Chaam Tak")
        {
            chaam = hit.collider;
            Debug.Log("click chaam complete");
            //set Chaam skill Panel
            skillPanel.SetActive(true);
            popUpIndex++;
        }

    }
}
