using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstScene; // Escena del primer nivel
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject controls;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    // Método que se ejecuta al inicio
    private void Start()
    {
        musicSlider.value = AudioManager.GetInstance().GetMusicVolume();
        soundSlider.value = AudioManager.GetInstance().GetSFXVolume();
    }

    public void PlayGame()
    {
        // Cargar la escena del menú principal
        //string scenePath = AssetDatabase.GetAssetPath(firstScene);
        //string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        ChangeScenes.LoadScene(firstScene);
    }

    public void QuitGame()
    {
        // Salir del juego
        Application.Quit();
    }

    public void SettingsPanel()
    {
        // Cargar panel opciones
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsReturn()
    {
        // Regresar al menú principal
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void AbrirControles ()
    {
        // Regresar al menú principal
        controls.SetActive(true);
        main.SetActive(false);
    }

    public void CerrarControles()
    {
        // Regresar al menú principal
        controls.SetActive(false);
        main.SetActive(true);
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
