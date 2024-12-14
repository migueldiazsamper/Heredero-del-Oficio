using System;
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
    private static TransitionImage transitionImage;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destruye este componente si ya hay una instancia activa.
            Destroy( this );
        }
        transitionImage = FindAnyObjectByType<TransitionImage>(); //Encuentra el único script TransitionImage que habrá en la escena
    }

    /// <summary>
    /// Carga la siguiente escena según el índice de construcción actual.
    /// </summary>
    
    public static void LoadNextScene ()
    {
        // Calcula el índice de la siguiente escena.
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;


        SceneManager.LoadScene( nextSceneIndex );        
        
    }




    /// <summary>
    /// Carga una escena específica por su nombre.
    /// </summary>
    /// <param name="sceneName">El nombre de la escena que se cargará.</param>
    
    public static void LoadScene ( string sceneName )
    {
        //Activa la animación de transición de escena
        instance.StartCoroutine(SceneChangeTransition(() =>
        {
            // Carga la escena correspondiente
            SceneManager.LoadScene( sceneName );
        }));   
    }

    /// <summary>
    /// Llama a la animación de transición de escena y espera a que termine
    /// </summary>
    /// <param name="afterDelay">Método void al que se llamará después de la espera</param>
    private static IEnumerator SceneChangeTransition(Action afterDelay){

        transitionImage.OnSceneChange();
        yield return new WaitForSeconds(1f);
        afterDelay?.Invoke();
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
