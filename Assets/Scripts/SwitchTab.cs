using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTab : MonoBehaviour
{
    private Transform[] tab;
    // List<Transform> main = new List<Transform>();
    // List<Transform> support = new List<Transform>();
    // List<Transform> chaam = new List<Transform>();
    //* เดี๋ยวจะกลับมาใช้ 
    Button mainButton;
    Button supportButton;
    Button chaamButton;
    Transform main;
    Transform chaam;
    Transform support;
    // Start is called before the first frame update
    void Start()
    {
        tab = gameObject.GetComponentsInChildren<Transform>();

        foreach (var t in tab)
        {
            // Debug.Log(t.name);
            if ((t.name).Contains("Main") && !(t.name).Contains("Select"))
            {
                if ((t.name).Contains("Button"))
                {
                    mainButton = t.GetComponent<Button>();
                    continue;
                }
                // main.Add(t);
                else if (t.name == "MainCharacter")
                {
                    main = t;
                }
            }
            else if ((t.name).Contains("Support") && !(t.name).Contains("Select"))
            {
                if ((t.name).Contains("Button"))
                {
                    supportButton = t.GetComponent<Button>();
                    continue;
                }
                // support.Add(t);
                else if (t.name == "SupportCharacter")
                {
                    support = t;
                }
            }
            else if ((t.name).Contains("Chaam") && !(t.name).Contains("Select"))
            {
                if ((t.name).Contains("Button"))
                {
                    chaamButton = t.GetComponent<Button>();
                    continue;
                }
                // chaam.Add(t);
                else if (t.name == "ChaamCharacter")
                {
                    chaam = t;
                }
            }
        }
        Debug.Log(chaam);
        Debug.Log(main);
        Debug.Log(support);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chaamButton.onClick.AddListener(() => Switch(chaamButton));
            mainButton.onClick.AddListener(() => Switch(mainButton));
            supportButton.onClick.AddListener(() => Switch(supportButton));
        }
    }

    public void Switch(Button button)
    {
        if (button.name == "ChaamButton")
        {
            chaam.gameObject.SetActive(true);
            main.gameObject.SetActive(false);
            support.gameObject.SetActive(false);
        }
        else if (button.name == "MainButton")
        {
            chaam.gameObject.SetActive(false);
            main.gameObject.SetActive(true);
            support.gameObject.SetActive(false);
        }
        if (button.name == "SupportButton")
        {
            chaam.gameObject.SetActive(false);
            main.gameObject.SetActive(false);
            support.gameObject.SetActive(true);
        }
    }
}
