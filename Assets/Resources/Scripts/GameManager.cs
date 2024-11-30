using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int gamePhase = 0;

    public static GameManager GetInstance ()
    {
        return instance;
    }

    private void Awake ()
    {
        if ( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy( this.gameObject );
        }
    }
}
