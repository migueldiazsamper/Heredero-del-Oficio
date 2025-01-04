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

    [SerializeField] private GameObject audioManagerPrefab;

    private void Awake()
    {
        // Comprobar si existe un AudioManager en la escena
        if (AudioManager.GetInstance() == null)
        {
            Instantiate(audioManagerPrefab);
        }
    }

    private void Start()
    {
        musicSlider.value = AudioManager.GetInstance().GetMusicVolume();
        soundSlider.value = AudioManager.GetInstance().GetSFXVolume();
    }
    
    // Método para ir al menú principal
    public void GoToMainMenu()
    {
        ReanudarJuego();

        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        settings.SetActive(false);
    }

    public void ReanudarJuego()
    {
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
        // Cargar panel opciones
        pause.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsReturn()
    {
        // Regresar al menú principal
        pause.SetActive(true);
        settings.SetActive(false);
    }

    public void playButtonSound()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick, AudioManager.GetInstance().buttonClickVolume);
    }

    public void ChangeMusicVolume()
    {
        // Cambiar volumen de la música 
        AudioManager.GetInstance().SetMusicVolume(musicSlider.value);
    }

    public void ChangeSoundVolume()
    {
        // Cambiar volumen de la música
        AudioManager.GetInstance().SetSFXVolume(soundSlider.value);
        if(SceneManager.GetActiveScene().name == "Pueblo" || SceneManager.GetActiveScene().name == "Mina")
        {
            CharacterMovementSound.GetInstance().characterMovementSource.volume = soundSlider.value * 0.4f;
        }
    }
}