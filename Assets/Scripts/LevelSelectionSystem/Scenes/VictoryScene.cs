using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    private GameObject starL;
    private GameObject starR;
    private GameObject starM;

    private void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");
        foreach (GameObject star in stars)
        {
            if (star.name.contains("L"))
            {
                starL = star;
            }
            else if (star.name.contains("R"))
            {
                starR = star;
            }
            else if (star.name.contains("M"))
            {
                starM = star;
            }
        }
    }

    private void Update()
    {
        // TODO set stars to correct state 
        //* die = 0 -> 3 stars -> all star.GetComponent<Animator>().setTrigger("Start")
        //* die = 1 -> 2 stars -> starR is not trigger
        //* die = 2 -> 1 star -> starR and starM are not trigger
        //* die = 3 -> 0 stars -> no star is trigger
    }
}
