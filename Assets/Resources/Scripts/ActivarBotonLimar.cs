using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ActivarBotonLimar : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneToLoad; // Nombre de la escena a cargar
    private int contador; // Contador de veces que se ha limado el esmalte

    // Método Start que se ejecuta al inicio del juego
    void Start()
    {
        // Inicializa el contador con el número de hijos del GameObject "EsmalteSobrante"
        GameObject esmalteSobrante = GameObject.Find("EsmalteSobrante");
        if (esmalteSobrante != null)
        {
            contador = esmalteSobrante.transform.childCount;
        }

        // Desactiva el objeto al inicio
        gameObject.SetActive(false);
    }

    // Método para restar 1 al contador
    public void RestarContador()
    {
        if (--contador <= 0)
        {
            // Activa el objeto cuando el contador llegue a 0
            gameObject.SetActive(true);
        }
    }

    // Método para cambiar de escena
    public void CambiarEscena()
    {
        if (sceneToLoad != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneToLoad);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            SceneManager.LoadScene(sceneName);
        }
    }   
 
}
