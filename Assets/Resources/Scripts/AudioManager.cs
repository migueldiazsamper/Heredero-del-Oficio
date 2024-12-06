using UnityEngine;

/// <summary>
/// Clase para gestionar el audio del juego, incluyendo música de fondo y efectos de sonido (SFX).
/// Implementa un patrón Singleton para garantizar una instancia única.
/// </summary>

public class AudioManager : MonoBehaviour
{
    [ Header( "Audio Sources" ) ]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [ Header( "------- Audio Clips -------" ) ]
    public AudioClip backgroundMusic;
    public AudioClip buttonClick;
    public AudioClip positiveFeedback;
    public AudioClip negativeFeedback;

    [ Header( "Town" ) ]

    public AudioClip characterMovement;
    public AudioClip enterBuilding;
    public AudioClip enterMine;

    [ Header( "Diálogos" ) ]

    public AudioClip dialogueSound;
    public AudioClip voiceManel;
    public AudioClip voiceMaria;
    public AudioClip voiceLluna;
    public AudioClip voiceSabio;
    public AudioClip voiceCondesa;
    public AudioClip voiceDirector;
    public AudioClip voiceJuan;
    public AudioClip voiceLluisa;
    public AudioClip voiceZorro;
    public AudioClip voiceCandela;
    public AudioClip voiceAgustina;
    public AudioClip voiceFrancesc;
    public AudioClip voiceLluc;

    [ Header( "Minijuego Barro" ) ]

    public AudioClip grabSand;
    public AudioClip cleanSand;

    [ Header( "Minijuego Moldes" ) ]

    public AudioClip grabPiece;
    public AudioClip dropPiece;
    public AudioClip rotatePiece;

    [ Header( "Minijuego Piezas Frágiles" ) ]

    [ Header( "Minijuego Primera Cocción" ) ]

    public AudioClip grabWood;
    public AudioClip dropWood;
    public AudioClip burningWood;

    [ Header( "Minijuego Esmalte" ) ]

    public AudioClip mixingSpoonEnamel;
    public AudioClip enamelPiece;
    public AudioClip scratchEnamel;
    public AudioClip scratchBase;
    public AudioClip linkPoints;

    [ Header( "Minijuego Pigmentos" ) ]

    public AudioClip insertSpoon;
    public AudioClip putPigment;
    public AudioClip mixingSpoonPigments;

    [ Header( "Minijuego Segunda Cocción" ) ]

    public AudioClip movePuzzlePiece;

    // Instancia estática para implementar el patrón Singleton.
    public static AudioManager instance;

    /// <summary>
    /// Devuelve la instancia única de AudioManager.
    /// </summary>
    
    public static AudioManager GetInstance ()
    {
        return instance;
    }

    /// <summary>
    /// Método Awake: se ejecuta antes de Start. Configura la instancia Singleton
    /// y evita que se destruya al cambiar de escena.
    /// </summary>

    private void Awake ()
    {
        // Comprueba si ya existe una instancia de AudioManager.
        bool noExisteInstancia = instance == null;

        if ( noExisteInstancia )
        {
            instance = this;

            // Hace que este GameObject persista entre escenas.
            DontDestroyOnLoad( gameObject );
        }
        else
        {
            // Si ya hay una instancia, destruye este GameObject duplicado.
            Destroy( gameObject );
        }
    }

    /// <summary>
    /// Método Start: se ejecuta al inicio del juego.
    /// Reproduce la música de fondo si está configurada.
    /// </summary>

    private void Start ()
    {
        // Asigna el clip de música de fondo al AudioSource correspondiente.
        musicSource.clip = backgroundMusic;

        // Reproduce la música de fondo.
        musicSource.Play();
    }

    /// <summary>
    /// Reproduce un efecto de sonido (SFX) con el AudioSource de efectos.
    /// </summary>
    /// <param name="clip">El AudioClip que se reproducirá.</param>

    public void PlaySFX ( AudioClip clip )
    {
        SFXSource.PlayOneShot( clip );
    }
}
