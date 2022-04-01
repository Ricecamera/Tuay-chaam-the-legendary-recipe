using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaptoItemHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MaptoItem.Instance == null)
        {
            MaptoItem.Instance.Awake();
        }
    }
}
