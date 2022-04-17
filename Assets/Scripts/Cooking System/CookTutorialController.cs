using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookTutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] popUps;
    private int popUpIndex;

    [SerializeField]
    private GameObject supportPanel;

    [SerializeField]
    private GameObject skillPanel;

    [SerializeField]
    private GameObject[] tickCook;

    [SerializeField]
    private GameObject comboPanel;

    [SerializeField]
    private GameObject okButton;

    private Collider2D enemy;

    private Collider2D chaam;


    void Start()
    {
        popUpIndex = 0;
        supportPanel.SetActive(false);
        comboPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleTutorialCookSystem")
        {
            GameObject.FindGameObjectsWithTag("Chaam")[1].GetComponent<ChaamRender>().setGuage(100);
        }

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

        if (popUpIndex == 5)
        {
            comboPanel.SetActive(true);
        }

        if (popUpIndex == 6 && Input.GetMouseButtonDown(0))
        {
            clickPotato();
        }
    }

    public void clickCook()
    {
        if (popUpIndex == 1)
        {
            supportPanel.SetActive(true);
            popUpIndex++;
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
            skillPanel.SetActive(true);
            popUpIndex++;
        }

    }

    // public void clickSupport(string popUpInt, string tickCookPos)
    // {
    //     if (popUpIndex == int.Parse(popUpInt))
    //     {
    //         tickCook[int.Parse(tickCookPos)].SetActive(true);
    //     }
    // }

    public void clickHoney()
    {
        if (popUpIndex == 2)
        {
            tickCook[0].SetActive(true);
            popUpIndex++;
        }
    }

    public void clickLime()
    {
        if (popUpIndex == 3)
        {
            tickCook[1].SetActive(true);
            popUpIndex++;
        }
    }

    public void clickSalt()
    {
        if (popUpIndex == 4)
        {
            tickCook[2].SetActive(true);
            popUpIndex++;
        }
    }

    public void clickStart()
    {
        if (popUpIndex == 5)
        {
            popUpIndex++;
        }

    }

    public void clickPotato()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.GetComponent<PakRender>().Entity.EntityName == "Potato")
        {
            enemy = hit.collider;
            okButton.SetActive(true);
            popUpIndex++;
        }
    }

    public void clickOk()
    {
        if (popUpIndex == 7)
        {
            if (enemy != null)
            {
                chaam.GetComponent<PakRender>().Attack(enemy.GetComponent<PakRender>().GetPosition(), () =>
                {
                    enemy.GetComponent<PakRender>().healthSystem.TakeDamage(50);
                    enemy.GetComponent<PakRender>().healthSystem.healthBar.SetFill(0.1f);
                }, () =>
                {
                    popUpIndex++;
                });
                //chaam.GetComponent<PakRender>().SlideToPosition(enemy.GetComponent<PakRender>().GetPosition(),)

            }

        }

    }

}
