using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerPP : MonoBehaviour
{
    public static LevelManagerPP instance;

    public List<bool> unlockStatus { get; set; }

    public int thislevel { get; set; }

    //number of time map was open;
    public int mapArrived;
    public int winTime = 0;

    void Start()
    {
        unlockStatus = new List<bool>() { true, true, true, true };
        mapArrived = 2;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }

    }

    // private void Update()
    // {
    //     if (mapArrived < unlockStatus.Count
    //         && !unlockStatus[mapArrived])
    //     {
    //         unlockStatus[mapArrived] = true;
    //     }
    // }
}
