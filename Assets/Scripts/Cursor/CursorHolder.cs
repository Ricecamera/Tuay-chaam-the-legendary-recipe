using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CursorChaphu.Instance == null)
        {
            CursorChaphu.Instance.Awake();
        }
        CursorChaphu.Instance.SetCursor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
