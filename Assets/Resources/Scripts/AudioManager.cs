using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Clase para gestionar el audio del juego, incluyendo música de fondo y efectos de sonido (SFX).
/// Implementa un patrón Singleton para garantizar una instancia única.
/// </summary>

public class AudioManager : MonoBehaviour
{
    [ Header( "Audio Sources" ) ]
    public AudioSource musicSource; public float musicSourceVolume = 1.0f;
    public AudioSource SFXSource; public float SFXSourceVolume = 1.0f;

    [ Header( "------- Audio Clips -------" ) ]
    public AudioClip backgroundMusic;
    public AudioClip buttonClick; public float buttonClickVolume = 1.0f;
    public AudioClip buttonHover; public float buttonHoverVolume = 1.0f;

    [ Header( "Feedback" ) ]
    public AudioClip positiveFeedback; public float positiveFeedbackVolume = 1.0f;
    public AudioClip negativeFeedback; public float negativeFeedbackVolume = 1.0f;

    [ Header( "Town" ) ]

    public AudioClip enterBuilding; public float enterBuildingVolume = 1.0f;
    public AudioClip enterMine; public float enterMineVolume = 1.0f;

    [ Header( "Diálogos" ) ]

    public AudioClip dialogueSound; public float dialogueSoundVolume = 1.0f;
    public AudioClip brokenPiece; public float brokenPieceVolume = 1.0f;

    [ Header( "Minijuego Barro" ) ]

    public AudioClip grabSand; public float grabSandVolume = 1.0f;
    public AudioClip grabImpurity; public float grabImpurityVolume = 1.0f;

    [ Header( "Minijuego Moldes" ) ]

    public AudioClip grabPiece; public float grabPieceVolume = 1.0f;
    public AudioClip dropPiece; public float dropPieceVolume = 1.0f;
    public AudioClip rotatePiece; public float rotatePieceVolume = 1.0f;

    [ Header( "Minijuego Piezas Frágiles" ) ]

    public AudioClip leftBalance; public float leftBalanceVolume = 1.0f;
    public AudioClip rightBalance; public float rightBalanceVolume = 1.0f;

    [ Header( "Minijuego Primera Cocción" ) ]

    public AudioClip grabWood; public float grabWoodVolume = 1.0f;
    public AudioClip dropWood; public float dropWoodVolume = 1.0f;
    public AudioClip burningWood; public float burningWoodVolume = 1.0f;
    public AudioClip timeUpBell; public float timeUpBellVolume = 1.0f;

    [ Header( "Minijuego Esmalte" ) ]

    public AudioClip mixingSpoonEnamel; public float mixingSpoonEnamelVolume = 1.0f;
    public AudioClip grabEnamelPiece; public float grabEnamelPieceVolume = 1.0f;
    public AudioClip EnamelPiece; public float EnamelPieceVolume = 1.0f;
    public AudioClip scratchEnamel; public float scratchEnamelVolume = 1.0f;
    public AudioClip scratchBase; public float scratchBaseVolume = 1.0f;
    public AudioClip grabPoint; public float grabPointVolume = 1.0f;
    public AudioClip linkPoint; public float linkPointVolume = 1.0f;
    

    [ Header( "Minijuego Pigmentos" ) ]

    public AudioClip insertSpoon; public float insertSpoonVolume = 1.0f;
    public AudioClip putPigment; public float putPigmentVolume = 1.0f;
    public AudioClip mixingSpoonPigments; public float mixingSpoonPigmentsVolume = 1.0f;

    [ Header( "Minijuego Segunda Cocción" ) ]

    public AudioClip movePuzzlePiece; public float movePuzzlePieceVolume = 1.0f;



    // Instancia estática para implementar el patrón Singleton.
    public static AudioManager instance;

    // Referencia al AudioMixer para controlar el volumen de la música y los efectos.
    public AudioMixer audioMixer;

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

    public void PlayLoop ( AudioClip clip )
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    public void StopLoop ()
    {
        SFXSource.Stop();
        SFXSource.clip = null;
    }

    public void PlaySFX ( AudioClip clip, float volume )
    {
        SFXSource.PlayOneShot( clip, volume );
    } 

    //Métodos para cambiar volumenes usando AudioMixer

    public void SetMusicVolume ( float volume )
    {
        audioMixer.SetFloat( "MusicVolume", Mathf.Log10(volume) * 20 ); // Convierte el volumen de 0 a 1 a una escala logarítmica.
    }

    public void SetSFXVolume ( float volume )
    {
        audioMixer.SetFloat( "SFXVolume", Mathf.Log10(volume) * 20 ); // Convierte el volumen de 0 a 1 a una escala logarítmica.
    }

    public float GetMusicVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float volume);
        return Mathf.Pow(10, volume / 20); // Convierte el volumen de una escala logarítmica a 0 a 1.
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat("SFXVolume", out float volume);
        return Mathf.Pow(10, volume / 20); // Convierte el volumen de una escala logarítmica a 0 a 1.
    }

}
