using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    [SerializeField] private SceneAsset pauseMenuSceneAsset; // Referencia a la escena del menú de pausa
    [SerializeField] private SceneAsset mainMenuSceneAsset; // Referencia a la escena del menú principal
    private bool isPaused; // Variable para saber si el juego está pausado
    private string currentSceneName; // Nombre de la escena actual

    public string pauseMenuScene { get; private set; }
    public string mainMenuScene { get; private set; }

    // Método Awake para inicializar el Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el objeto al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método Start que se ejecuta al inicio
    void Start()
    {
        // Inicializar la variable isPaused
        isPaused = false;

        // Obtener los nombres de las escenas desde los SceneAssets
#if UNITY_EDITOR
        if (pauseMenuSceneAsset != null)
        {
            pauseMenuScene = AssetDatabase.GetAssetPath(pauseMenuSceneAsset);
            pauseMenuScene = System.IO.Path.GetFileNameWithoutExtension(pauseMenuScene);
        }

        if (mainMenuSceneAsset != null)
        {
            mainMenuScene = AssetDatabase.GetAssetPath(mainMenuSceneAsset);
            mainMenuScene = System.IO.Path.GetFileNameWithoutExtension(mainMenuScene);
        }
#endif
    }

    // Método Update que se ejecuta en cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Si se presiona la tecla Escape
        {
            // Verificar si la escena actual es el menú principal
            if (SceneManager.GetActiveScene().name == mainMenuScene)
            {
                return; // No abrir el menú de pausa si estamos en el menú principal
            }

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
            SceneManager.UnloadSceneAsync(pauseMenuScene);
        }
    }
}