using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Definición de la clase ChangeScenes que hereda de MonoBehaviour
public class ChangeScenes : MonoBehaviour
{
    // Método público que carga la siguiente escena en el índice de construcción
    public void LoadNextScene ()
    {
        // Carga la escena siguiente en el índice de construcción de escenas
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }
}