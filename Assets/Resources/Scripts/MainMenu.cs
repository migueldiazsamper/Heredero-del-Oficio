using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset firstScene; // Escena del primer nivel
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject controls;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    // El metodo Awake prepara los elementos necesarios al inicio del juego
    private void Start()
    {
        musicSlider.value = AudioManager.GetInstance().musicSource.volume;
        soundSlider.value = AudioManager.GetInstance().SFXSource.volume;
    }

    public void playButtonSound()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);
    }
    public void PlayGame()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Cargar la escena del menú principal
        string scenePath = AssetDatabase.GetAssetPath(firstScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        ChangeScenes.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Salir del juego
        Application.Quit();
    }

    public void SettingsPanel()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Cargar panel opciones
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsReturn()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Regresar al menú principal
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void AbrirControles ()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Regresar al menú principal
        controls.SetActive(true);
        settings.SetActive(false);
    }

    public void CerrarControles()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick);

        // Regresar al menú principal
        controls.SetActive(false);
        settings.SetActive(true);
    }

    public void ChangeMusicVolume()
    {
        // Cambiar volumen de la música
        AudioManager.GetInstance().musicSource.volume = musicSlider.value;    
    }

    public void ChangeSoundVolume()
    {
        // Cambiar volumen de la música
        AudioManager.GetInstance().SFXSource.volume = soundSlider.value; 
    }
}
