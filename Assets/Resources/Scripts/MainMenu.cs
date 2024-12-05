using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset firstScene; // Escena del primer nivel
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject main;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    // El metodo Awake prepara los elementos necesarios al inicio del juego
    private void Start()
    {
        AudioManager.GetInstance().musicSource.volume = musicSlider.value;
        AudioManager.GetInstance().SFXSource.volume = soundSlider.value;
    }

    public void PlayGame()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().testSFX);

        // Cargar la escena del menú principal
        string scenePath = AssetDatabase.GetAssetPath(firstScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().testSFX);

        // Salir del juego
        Application.Quit();
    }

    public void SettingsPanel()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().testSFX);

        // Cargar panel opciones
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsReturn()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().testSFX);

        // Regresar al menú principal
        main.SetActive(true);
        settings.SetActive(false);
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
