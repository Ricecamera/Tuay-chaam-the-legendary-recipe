using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapToEnemyHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MapToEnemy.Instance == null)
        {
            MapToEnemy.Instance.Awake();
        }
    }
}