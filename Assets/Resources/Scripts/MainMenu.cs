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

    public void PlayGame()
    {
        // Cargar la escena del menú principal
        string scenePath = AssetDatabase.GetAssetPath(firstScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        ChangeScenes.LoadScene(sceneName);
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
        settings.SetActive(false);
    }

    public void CerrarControles()
    {
        // Regresar al menú principal
        controls.SetActive(false);
        settings.SetActive(true);
    }

    public void playButtonSound()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick, AudioManager.GetInstance().buttonClickVolume);
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
        if(SceneManager.GetActiveScene().name == "Pueblo" || SceneManager.GetActiveScene().name == "Mina")
        {
            CharacterMovementSound.GetInstance().characterMovementSource.volume = soundSlider.value;
        }
    }
}
