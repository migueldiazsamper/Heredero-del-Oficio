using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// Clase para gestionar la activación de un botón al reducir un contador 
/// y para cambiar de escena cuando sea necesario.
/// </summary>

public class ActivarBotonLimar : MonoBehaviour
{
    // Referencia a la escena que se cargará al ejecutar el método `CambiarEscena`.
    [ SerializeField ]
    private SceneAsset sceneToLoad;

    // Contador que rastrea la cantidad de esmaltes sobrantes.
    private int contador;

    /// <summary>
    /// Método que se ejecuta al inicio del juego.
    /// Inicializa el contador con el número de hijos del GameObject "EsmalteSobrante"
    /// y desactiva este GameObject al comenzar.
    /// </summary>

    private void Start ()
    {
        // Busca el GameObject llamado "EsmalteSobrante" en la escena.
        GameObject esmalteSobrante = GameObject.Find( "EsmalteSobrante" );

        // Verifica si el GameObject "EsmalteSobrante" existe.
        bool esmalteEncontrado = esmalteSobrante != null;

        if ( esmalteEncontrado )
        {
            // Asigna al contador la cantidad de hijos directos de "EsmalteSobrante".
            contador = esmalteSobrante.transform.childCount;
        }
        else
        {
            // Si no se encuentra el GameObject, inicializa el contador en 0.
            contador = 0;
            Debug.LogWarning( "EsmalteSobrante no encontrado en la escena. Contador inicializado a 0." );
        }

        // Este GameObject (el botón) comienza desactivado.
        gameObject.SetActive( false );
    }

    /// <summary>
    /// Reduce el contador en 1 y activa este GameObject (el botón) si el contador llega a 0.
    /// </summary>
    
    public void RestarContador ()
    {
        // Disminuye el contador en 1.
        contador--;

        // Verifica si el contador llegó a 0 o menos.
        bool contadorVacio = contador <= 0;

        if ( contadorVacio )
        {
            gameObject.SetActive( true );
            Debug.Log( "Contador llegó a 0. Botón activado." );
        }
    }

    /// <summary>
    /// Cambia a la escena especificada en `sceneToLoad` si está configurada.
    /// </summary>

    public void CambiarEscena ()
    {
        // Verifica si hay una escena asignada para cargar.
        bool escenaAsignada = sceneToLoad != null;

        if ( escenaAsignada )
        {
            // Obtiene la ruta del asset de la escena seleccionada.
            string scenePath = AssetDatabase.GetAssetPath( sceneToLoad );

            // Extrae el nombre de la escena desde la ruta.
            string sceneName = System.IO.Path.GetFileNameWithoutExtension( scenePath );

            // Carga la escena especificada.
            SceneManager.LoadScene( sceneName );
        }
        else
        {
            // Lanza un mensaje de error si no hay escena asignada.
            Debug.LogError( "Scene to load no está asignada en el inspector." );
        }
    }
}
