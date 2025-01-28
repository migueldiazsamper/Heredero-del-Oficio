using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instrucciones : MonoBehaviour
{
    public Canvas mainCanvas; // Asigna el Main Canvas aquí
    public Canvas instructionCanvas;  // Canvas de instrucciones
    public Image[] instructionImages;  // Array de imágenes de instrucciones
    public Button nextButton;          // Botón para pasar a la siguiente instrucción
    public Button backButton;          // Botón para retroceder
    public Button startButton;         // Botón para iniciar el minijuego

    private int currentIndex = 0;      // Índice de la imagen actual

    void Start()
    {
        // Desactivar el Main Canvas al iniciar
        mainCanvas.gameObject.SetActive(false);

        // Asegurarse de que el Canvas esté activo al inicio
        instructionCanvas.gameObject.SetActive(true);

        // Pausar el tiempo del juego
        //Time.timeScale = 0;

        // Configurar el estado inicial
        currentIndex = 0;
        UpdateInstructions();

        if(SceneManager.GetActiveScene().name == "Minijuego 3" && PhasesManager.instance.hasMinigame3HappenedAlready) this.enabled = false;
        else PhasesManager.instance.hasMinigame3HappenedAlready = true;

        if(SceneManager.GetActiveScene().name == "Minijuego 6" && PhasesManager.instance.hasMinigame6HappenedAlready) this.enabled = false;
        else PhasesManager.instance.hasMinigame6HappenedAlready = true;
    }

    public void NextInstruction()
    {
        if (currentIndex < instructionImages.Length - 1)
        {
            currentIndex++;
            UpdateInstructions();
        }
    }

    public void PreviousInstruction()
{
    if (currentIndex > 0)
    {
        currentIndex--;
        Debug.Log("Retrocediendo a la instrucción " + currentIndex);  // Debug para comprobar el índice
        UpdateInstructions();
    }
    else
    {
        Debug.Log("No puedes retroceder, ya estás en la primera instrucción.");
    }
}

    public void StartGame()
    {
        // Activar el Main Canvas y desactivar el de instrucciones
        mainCanvas.gameObject.SetActive(true);
        instructionCanvas.gameObject.SetActive(false);

        // Reactivar el tiempo y ocultar las instrucciones
        //Time.timeScale = 1;
    }

    private void UpdateInstructions()
{
    Debug.Log("Actualizando instrucciones, mostrando instrucción " + currentIndex);

    // Mostrar solo la imagen actual
    for (int i = 0; i < instructionImages.Length; i++)
    {
        instructionImages[i].gameObject.SetActive(i == currentIndex);
    }

    // Configurar botones
    backButton.gameObject.SetActive(currentIndex > 0);
    nextButton.gameObject.SetActive(currentIndex < instructionImages.Length - 1);
    startButton.gameObject.SetActive(currentIndex == instructionImages.Length - 1);
}
}

