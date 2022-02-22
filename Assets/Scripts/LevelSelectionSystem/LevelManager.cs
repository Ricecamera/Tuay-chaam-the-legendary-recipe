using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<bool> unlockStatus { get; set; }

    public int thislevel { get; set; }

    void Start()
    {
        unlockStatus = new List<bool>() { true, false, false, false };

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





}
