using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhasesManager : MonoBehaviour
{
    public static PhasesManager instance;

    public int currentPhase = 0;
    public int maxPhases = 16;
    [SerializeField] private int puntuacionTotal = 0;
    [SerializeField] private int puntuacionCondesa = 80;
    public bool nextIsPueblo = false;
    public int vecesMina = 0;
    public bool tieneQueHablarConSabio = false;
    public Vector2[] zonasDeAparicion = new Vector2[4];

    //Los 3 colores que se usarán para pintar la mancerina final
    private static Color primerColor;
    private static Color segundoColor;
    private static Color tercerColor;
    public Color[] coloresMancerina = {primerColor, segundoColor, tercerColor};
    public int savedColors = 0;

    private void Awake()
    {
        // Implementación del patrón singleton
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
        zonasDeAparicion[0] = new Vector2(44.95f, 81.85f); // Casa
        zonasDeAparicion[1] = new Vector2(-9.83f, 47.679f); // Mina
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

    public int PuntuacionTotal(){
        return puntuacionTotal;
    }

    public int PuntuacionCondesa(){
        return puntuacionCondesa;
    }

    public void AddScore(int amount){
        puntuacionTotal += amount;
    }
}
