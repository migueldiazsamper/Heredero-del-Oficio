using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset mainMenuSceneAsset; // Referencia a la escena del menú principal
    [SerializeField] private SceneAsset pauseMenuSceneAsset; // Referencia a la escena del menú de pausa
    private bool isPaused; // Variable para saber si el juego está pausado
    private string currentSceneName; // Nombre de la escena actual

    private string mainMenuScene;
    private string pauseMenuScene;

    // Método Start que se ejecuta al inicio
    void Start()
    {
        // Inicializar la variable isPaused
        isPaused = false;

        // Obtener los nombres de las escenas desde los SceneAssets
        if (mainMenuSceneAsset != null)
        {
            mainMenuScene = AssetDatabase.GetAssetPath(mainMenuSceneAsset);
            mainMenuScene = System.IO.Path.GetFileNameWithoutExtension(mainMenuScene);
        }

        if (pauseMenuSceneAsset != null)
        {
            pauseMenuScene = AssetDatabase.GetAssetPath(pauseMenuSceneAsset);
            pauseMenuScene = System.IO.Path.GetFileNameWithoutExtension(pauseMenuScene);
        }
    }

    // Método Update que se ejecuta en cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Si se presiona la tecla Escape
        {
            if (isPaused) // Si el juego está pausado
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
        // Pausar el juego
        Time.timeScale = 0f;
        isPaused = true;

        // Guardar la escena actual
        currentSceneName = SceneManager.GetActiveScene().name;

        // Cargar la escena del menú de pausa
        if (!string.IsNullOrEmpty(pauseMenuScene))
        {
            SceneManager.LoadScene(pauseMenuScene, LoadSceneMode.Additive);
        }
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        isPaused = false;

        // Descargar la escena del menú de pausa
        if (!string.IsNullOrEmpty(pauseMenuScene))
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(pauseMenuScene);
            if (asyncOperation != null)
            {
                asyncOperation.completed += (AsyncOperation op) =>
                {
                    // Asegurarse de que la escena del menú de pausa se descargue correctamente
                };
            }
        }
    }

    public void GoToMainMenu()
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        isPaused = false;

        // Cargar la escena del menú principal
        if (!string.IsNullOrEmpty(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}