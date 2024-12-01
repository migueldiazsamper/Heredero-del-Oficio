using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhasesManager : MonoBehaviour
{
    public static PhasesManager instance;

    public int currentPhase = 0;
    public int maxPhases = 16;
    public int puntuacionTotal = 0;
    public bool nextIsPueblo = false;
    public int vecesMina = 0;
    public bool tieneQueHablarConSabio = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void NextPhase()
    {
        if (currentPhase < maxPhases)
        {
            if (++currentPhase == 8)
            {
                if ( currentPhase == 8 )
                {
                    tieneQueHablarConSabio = true;
                }
            }
        }
    }
}
