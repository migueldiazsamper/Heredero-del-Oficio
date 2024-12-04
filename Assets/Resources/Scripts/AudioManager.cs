using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip testSFX;

    public static AudioManager instance; // Propiedad para acceder a la instancia

    public static AudioManager GetInstance()
    {
        return instance;
    }

    // El metodo Awake se ejecuta antes del Start
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // El meto Start se ejecuta al inicio del juego
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // El metodo PlaySFX activa un sonido en el juego
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
