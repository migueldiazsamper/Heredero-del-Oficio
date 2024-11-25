using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider; // Referencia al slider del volumen de la música
    [SerializeField] private Slider sfxVolumeSlider; // Referencia al slider del volumen de los efectos de sonido

    // Método para reanudar el juego
    public void ResumeGame()
    {
        PauseMenuManager.Instance.ResumeGame();
    }

    // Método para ir al menú principal
    public void GoToMainMenu()
    {
        PauseMenuManager.Instance.ResumeGame();

        // Buscar y destruir el objeto AudioManager
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager != null)
        {
            Destroy(audioManager);
        }

        SceneManager.LoadScene(PauseMenuManager.Instance.mainMenuScene);
    }

}