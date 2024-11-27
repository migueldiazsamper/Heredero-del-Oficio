using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Referencia al menú de pausa
    [SerializeField] private GameObject[] objetos; // Referencia al objeto de los puntos

    // Método para ir al menú principal
    public void GoToMainMenu()
    {
        ReanudarJuego();

        // Buscar y destruir el objeto AudioManager
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager != null)
        {
            Destroy(audioManager);
        }

        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    public void OcultarObjetos()
    {
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(false);
        }
    }

    public void MostrarObjetos()
    {
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(true);
        }
    }
}