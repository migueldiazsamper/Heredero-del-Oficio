using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;

    // El metodo Awake se ejecuta antes del Start
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
