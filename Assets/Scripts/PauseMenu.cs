using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Referencia al menú de pausa
    [SerializeField] private SceneAsset mainMenuScene; // Escena del menú principal
    [SerializeField] private bool isPaused; // Variable para saber si el juego está pausado

    // Método Start que se ejecuta al inicio
    void Start()
    {
        // Desactivar el menú de pausa al inicio
        pauseMenu.SetActive(false); 

        // Inicializar la variable isPaused
        isPaused = false; 
    }

    // Método Update que se ejecuta en cada frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Si se presiona la tecla Escape
        {
            if(isPaused) // Si el juego está pausado
            {
                ResumeGame(); // Reanudar el juego
            }
            else // Si el juego no está pausado
            {
                PauseGame(); // Pausar el juego
            }
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        // Activar el menú de pausa
        pauseMenu.SetActive(true);
        // Pausar el juego
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        // Desactivar el menú de pausa
        pauseMenu.SetActive(false);
        // Reanudar el juego
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        isPaused = false;

        // Cargar la escena del menú principal
        string scenePath = AssetDatabase.GetAssetPath(mainMenuScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
    }
}
