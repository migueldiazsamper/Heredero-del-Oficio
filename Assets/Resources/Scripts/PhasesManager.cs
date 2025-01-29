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
    public string currentQuest = "Fabrica";

    public bool hasMinigame3HappenedAlready = false;
    public bool hasMinigame6HappenedAlready = false;

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


    private void Start(){
        currentPhase = 0;
        maxPhases = 16;
        puntuacionTotal = 0;
        puntuacionCondesa = 80;
        nextIsPueblo = false;
        vecesMina = 0;
        tieneQueHablarConSabio = false;
        zonasDeAparicion = new Vector2[4];
        zonasDeAparicion[0] = new Vector2(44.95f, 81.85f); // Casa
        zonasDeAparicion[1] = new Vector2(-9.83f, 47.679f); // Mina
        zonasDeAparicion[2] = new Vector2(1.55f, 98.5f); // Fábrica
        zonasDeAparicion[3] = new Vector2(28.1f, 117.0f); // Hornos
        primerColor = new Color();
        segundoColor = new Color();
        tercerColor = new Color();
        coloresMancerina = new Color[] { primerColor, segundoColor, tercerColor };
        savedColors = 0;

    


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu"){
            // Add your code here that should run when the scene loads
            Debug.Log("Scene loaded: " + scene.name);
            
            currentPhase = 0;
            maxPhases = 16;
            puntuacionTotal = 0;
            puntuacionCondesa = 80;
            nextIsPueblo = false;
            vecesMina = 0;
            tieneQueHablarConSabio = false;
            zonasDeAparicion = new Vector2[4];
            zonasDeAparicion[0] = new Vector2(43.71f, 81.97f); // Casa
            zonasDeAparicion[1] = new Vector2(-9.83f, 47.679f); // Mina
            zonasDeAparicion[2] = new Vector2(1.55f, 98.5f); // Fábrica
            zonasDeAparicion[3] = new Vector2(28.1f, 117.0f); // Hornos
            primerColor = new Color();
            segundoColor = new Color();
            tercerColor = new Color();
            coloresMancerina = new Color[] { primerColor, segundoColor, tercerColor };
            savedColors = 0;
        }

        if (SceneManager.GetActiveScene().name == "Minijuego 3") {
            Debug.Log(SceneManager.GetActiveScene().name + " ESTE ES EL MINIJUEGO 3"); 
            if(hasMinigame3HappenedAlready) GameObject.Find("Instrucciones Canvas").SetActive(false);
            else hasMinigame3HappenedAlready = true;
        }
        if (SceneManager.GetActiveScene().name == "Minijuego 6") {
            Debug.Log(SceneManager.GetActiveScene().name + " ESTE ES EL MINIJUEGO 6");
            if(hasMinigame6HappenedAlready) GameObject.Find("Instrucciones Canvas").SetActive(false);
            else hasMinigame6HappenedAlready = true;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void NextPhase()
    {
        int nextPhase = currentPhase + 1;
        switch(nextPhase)
        {
            case 2:
                currentQuest = "Mina";           
            break;
            case 8:
                currentQuest = "Viejo";
            break;
            case 10:
                currentQuest = "Fabrica";
            break;
            case 12:
                currentQuest = "Tienda";
            break;
            case 14:
                currentQuest = "Hornos";
            break;
            case 16:
                currentQuest = "Palacio";
            break;
            default:
            break;
        }

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
