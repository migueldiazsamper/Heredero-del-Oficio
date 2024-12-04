using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Referencia al menú de pausa
    [SerializeField] private GameObject[] objetos; // Referencia al objeto de los puntos

    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject pause;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        musicSlider.value = audioManager.musicSource.volume;
        soundSlider.value = audioManager.SFXSource.volume; 
    }
    
    // Método para ir al menú principal
    public void GoToMainMenu()
    {
        ReanudarJuego();

        // Buscar y destruir el objeto AudioManager
        GameObject audioManagerCopy = GameObject.Find("AudioManager");
        if (audioManagerCopy != null)
        {
            Destroy(audioManagerCopy);
        }

        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void PausarJuego()
    {
        // Reproducir sonido botón
        audioManager.PlaySFX(audioManager.testSFX);

        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        settings.SetActive(false);
    }

    public void ReanudarJuego()
    {
        // Reproducir sonido botón
        audioManager.PlaySFX(audioManager.testSFX);

        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    public void OcultarObjetos()
    {
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(false);
        }
    }

    public void MostrarObjetos()
    {
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(true);
        }
    }

    public void SettingsPanel()
    {
        // Reproducir sonido botón
        audioManager.PlaySFX(audioManager.testSFX);

        // Cargar panel opciones
        pause.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsReturn()
    {
        // Reproducir sonido botón
        audioManager.PlaySFX(audioManager.testSFX);

        // Regresar al menú principal
        pause.SetActive(true);
        settings.SetActive(false);
    }

    public void ChangeMusicVolume()
    {
        // Cambiar volumen de la música
        audioManager.musicSource.volume = musicSlider.value;    
    }

    public void ChangeSoundVolume()
    {
        // Cambiar volumen de la música
        audioManager.SFXSource.volume = soundSlider.value;    
    }
}