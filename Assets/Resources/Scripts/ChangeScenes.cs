using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase para gestionar los cambios de escena en un juego de Unity.
/// Implementa un patrón Singleton para facilitar su acceso.
/// </summary>

public class ChangeScenes : MonoBehaviour
{
    // Instancia estática para implementar el patrón Singleton.
    public static ChangeScenes instance;

    /// <summary>
    /// Método Awake: asegura que solo exista una instancia de esta clase.
    /// </summary>
    
    private void Awake ()
    {
        // Verifica si ya existe una instancia.
        bool noExisteInstancia = instance == null;

        if ( noExisteInstancia )
        {
            instance = this;
        }
        else
        {
            // Destruye este componente si ya hay una instancia activa.
            Destroy( this );
        }
    }

    /// <summary>
    /// Carga la siguiente escena según el índice de construcción actual.
    /// </summary>
    
    public static void LoadNextScene ()
    {
        // Calcula el índice de la siguiente escena.
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Carga la escena correspondiente al índice calculado.
        SceneManager.LoadScene( nextSceneIndex );
    }

    /// <summary>
    /// Carga una escena específica por su nombre.
    /// </summary>
    /// <param name="sceneName">El nombre de la escena que se cargará.</param>
    
    public static void LoadScene ( string sceneName )
    {
        
        SceneManager.LoadScene( sceneName );
    }


    private IEnumerator SceneChangeAnimation(){

        
        yield return new WaitForSeconds(1f);

    }

    /// <summary>
    /// Recarga la escena actual.
    /// </summary>
    
    public void ReloadScene ()
    {
        // Obtiene la escena activa actual.
        Scene currentScene = SceneManager.GetActiveScene();

        // Carga nuevamente la escena actual por su nombre.
        SceneManager.LoadScene( currentScene.name );
    }
}
