using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset firstScene; // Escena del primer nivel

    public void PlayGame()
    {
        // Cargar la escena del menú principal
        string scenePath = AssetDatabase.GetAssetPath(firstScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
