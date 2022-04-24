using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] popUps;
    private int popUpIndex;

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

    }
}
