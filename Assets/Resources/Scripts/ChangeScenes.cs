using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Esta clase permite cambiar de escena en un juego de Unity
public class ChangeScenes : MonoBehaviour
{
    public static ChangeScenes instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Este método público carga la siguiente escena en el índice de construcción
    public static void LoadNextScene ()
    {
        // Obtiene el índice de la escena actual y le suma 1 para obtener el índice de la siguiente escena
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Carga la escena que corresponde al índice calculado
        SceneManager.LoadScene( nextSceneIndex );
    }

    public static void LoadScene ( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }

    
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}