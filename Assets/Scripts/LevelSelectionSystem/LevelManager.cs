using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<bool> unlockStatus { get; set; } = new List<bool>() { false, false, false, false, false, false, false, false };

    public List<bool> playAniAlreadyMap { get; set; }

    public int thislevel;

    //number of time map was open;
    public int mapArrived;
    public int winTime = 0;

    void Start()
    {
        // unlockStatus = new List<bool>() { true, true, true, true, true, true, true, true };
        for (int i = 0; i < SaveManager.instance.playerDatabase.unlockStatus; i++)
        {
            unlockStatus[i] = true;
        }
        // unlockStatus.AddRange(SaveManager.instance.playerDatabase.unlockStatus);
        playAniAlreadyMap = new List<bool>() { false, false, false, false, false, false, false, false };
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