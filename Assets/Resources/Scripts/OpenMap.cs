using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [ SerializeField ] private GameObject map;
    public static bool isShowingMap = false;


    public void Open()
    {
        isShowingMap = true;
        Time.timeScale = 0;
        map.SetActive( true );
    }

    public void Close()
    {
        isShowingMap = false;
        Time.timeScale = 1;
        map.SetActive( false );
    }
}
