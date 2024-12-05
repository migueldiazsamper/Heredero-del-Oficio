using System.Collections;
using UnityEngine;

// Clase ConstantRotation que maneja la rotación continua de un objeto en Unity
public class ConstantRotation : MonoBehaviour
{
    // Velocidad de rotación ajustable
    [ SerializeField ] 
    private float difficulty;

    // Intervalo de cambio al introducir input
    [ SerializeField ] 
    private float inputInterval;

    // Ángulo de rotación
    private float angle;

    // Intervalo de pausa para la corrutina
    private float updateInterval = 0.1f;

    // Indicador de finalización de la rotación
    private bool Isfinish;

    /// <summary>
    /// Método para obtener el ángulo actual de rotación
    /// </summary>
    
    public float GetAngle ()
    {
        return angle;
    }

    /// <summary>
    /// Método para obtener la dificultad actual
    /// </summary>
    
    public float GetDifficulty ()
    {
        return difficulty;
    }

    /// <summary>
    /// Finaliza el proceso de rotación
    /// </summary>
    
    public void MakeFinish ()
    {
        Isfinish = true;
    }

    /// <summary>
    /// Actualiza la rotación del GameObject usando interpolación suave
    /// </summary>
    
    private void UpdateGameObjectRotation ()
    {
        // Rotación deseada del GameObject
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Interpolación suave de la rotación del GameObject usando Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * difficulty);
    }

    /// <summary>
    /// Corrutina que varía el ángulo de rotación
    /// </summary>
    
    IEnumerator VariationCoroutine ()
    {
        // Bucle infinito hasta que pierdas o ganes
        while ( !Isfinish )
        {
            // Ajusta el ángulo según su inclinación
            bool isAnglePositive = angle > 0;
            if ( isAnglePositive )
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

    /// <summary>
    /// Método Start que se llama al iniciar el script
    /// </summary>
    
    void Start ()
    {
        // Inicia el ángulo en 0º y establece que el juego no está en estado terminado
        Isfinish = false;
        angle = 0.0f;

        // Inicia la corrutina de variación de rotación
        StartCoroutine(VariationCoroutine());
    }

    /// <summary>
    /// Método Update que se llama una vez por frame
    /// </summary>
    
    void Update ()
    {
        // Detecta si se ha presionado la tecla derecha o izquierda
        bool rightArrowIsDown = Input.GetKeyDown(KeyCode.RightArrow);
        bool leftArrowIsDown = Input.GetKeyDown(KeyCode.LeftArrow);

        bool isNotFinished = !Isfinish;

        // Ajusta el ángulo si la tecla derecha es presionada
        if ( rightArrowIsDown && isNotFinished )
        {
            angle -= inputInterval;
        }
        // Ajusta el ángulo si la tecla izquierda es presionada
        else if ( leftArrowIsDown && isNotFinished )
        {
            angle += inputInterval;
        }

        // Actualiza la rotación del GameObject
        UpdateGameObjectRotation();
    }

    /// <summary>
    /// Método para manejar el clic izquierdo
    /// </summary>
    
    public void OnLeftButtonClick ()
    {
        if ( !Isfinish )
        {
            angle += inputInterval;
            UpdateGameObjectRotation();
        }
    }

    /// <summary>
    /// Método para manejar el clic derecho
    /// </summary>
    
    public void OnRightButtonClick ()
    {
        if ( !Isfinish )
        {
            angle -= inputInterval;
            UpdateGameObjectRotation();
        }
    }
}
