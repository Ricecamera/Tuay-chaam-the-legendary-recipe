using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChaphu : MonoBehaviour
{
    private static CursorChaphu instance;
    public Texture2D cursorArrow;

    public static CursorChaphu Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("CursorChaphu");
                go.AddComponent<CursorChaphu>();
                go.GetComponent<CursorChaphu>().cursorArrow = Resources.Load<Texture2D>("Chaphu2");
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    public void Awake()
    {
        instance = this;
    }

    public void SetCursor()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}

