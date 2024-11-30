using System.Collections;
using UnityEngine;

// Definici�n de la clase ConstantRotation que hereda de MonoBehaviour
public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float difficulty; // Velocidad de rotación ajustable
    [SerializeField] private float inputInterval; // Intervalo de cambio al introducir input
    private float angle; // Ángulo de rotación
    private float updateInterval = 0.1f; // Intervalo de pausa para la corrutina
    private bool Isfinish; // Indicador de finalización

    // M�todo para obtener el �ngulo actual
    public float GetAngle()
    {
        return angle;
    }

    // M�todo para obtener la dificultad actual
    public float GetDifficulty()
    {
        return difficulty;
    }

    // M�todo para finalizar la rotaci�n
    public void MakeFinish()
    {
        Isfinish = true;
    }

    // M�todo que actualiza la rotaci�n del GameObject
    private void UpdateGameObjectRotation()
    {
        // Rotación deseada del GameObject
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Interpolación suave de la rotación del GameObject usando Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * difficulty);
    }

    // Corrutina que varía el ángulo de rotación
    IEnumerator VariationCoroutine()
    {
        // Bucle infinito hasta que pierdas o ganes
        while (!Isfinish)
        {          
            // Ajusta el ángulo según su inclinación para que se aleje del punto de equilibrio
            if (angle > 0)
            {
                angle += 5f;
            }
            else
            {
                angle -= 5f;
            }

            // Actualiza la rotación del GameObject
            UpdateGameObjectRotation();

            // Espera el tiempo del intervalo de pausa antes de continuar
            yield return new WaitForSeconds(updateInterval);
        }
    }

    // M�todo Start que se llama al iniciar el script
    void Start()
    {
        // Inicia el �ngulo en 0� y establece que el juego no est� en estado terminado
        Isfinish = false;
        angle = 0.0f;

        // Inicia la corrutina de variaci�n de rotaci�n
        StartCoroutine(VariationCoroutine());
    }

    // M�todo Update que se llama una vez por frame
    void Update()
    {
        // Actualiza la rotación del GameObject
        // UpdateGameObjectRotation();

        // Detecta si has pulsado la tecla Derecha o Izquierda para ajustar el �ngulo adecuadamente
        bool rightArrowIsDown = Input.GetKeyDown(KeyCode.RightArrow);
        bool leftArrowIsDown = Input.GetKeyDown(KeyCode.LeftArrow);

        if (rightArrowIsDown && !Isfinish)
        {
            angle -= inputInterval;
        }

        else if (leftArrowIsDown && !Isfinish)
        {
            angle += inputInterval;
        }

        // Actualiza la rotación del GameObject
        UpdateGameObjectRotation();
    }

    // Método para manejar click izquierdo
    public void OnLeftButtonClick()
    {
        if (!Isfinish)
        {
            angle += inputInterval;
            UpdateGameObjectRotation();
        }
    }

    // Método para manejar click derecho
    public void OnRightButtonClick()
    {
        if (!Isfinish)
        {
            angle -= inputInterval;
            UpdateGameObjectRotation();
        }
    }

}