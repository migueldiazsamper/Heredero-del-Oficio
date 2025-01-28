using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture1;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public static CursorManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode);
    }

    void Update ()
    {
        if ( Input.GetMouseButtonDown( 0 )  )
        {
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
        }
        else if ( Input.GetMouseButtonUp( 0 ) )
        {
            Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode);
        }
    }
}
