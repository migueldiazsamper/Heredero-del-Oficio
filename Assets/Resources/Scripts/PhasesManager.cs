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
    public Vector2[] zonasDeAparicion = new Vector2[4];

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        zonasDeAparicion[0] = new Vector2(44.95f, 81.85f); // Casa
        zonasDeAparicion[1] = new Vector2(-9.5f, 49.0f); // Mina
        zonasDeAparicion[2] = new Vector2(1.55f, 98.5f); // Fábrica
        zonasDeAparicion[3] = new Vector2(28.1f, 117.0f); // Hornos
    }

    public void NextPhase()
    {
        if (currentPhase < maxPhases)
        {
            if (++currentPhase == 8)
            {
                /* if ( currentPhase == 8 )
                {
                    tieneQueHablarConSabio = true;
                } */
                tieneQueHablarConSabio = true;
            }
        }
    }
}
