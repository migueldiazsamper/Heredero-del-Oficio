using UnityEngine;

// Definici�n de la clase CheckIfDefeat que hereda de MonoBehaviour
public class CheckIfDefeat : MonoBehaviour
{
    // Referencia al componente ConstantRotation
    private ConstantRotation constantRotation;

    // Intervalo de derrota (1s) y temporizador
    [SerializeField] private float defeatInterval = 10f;
    private float timer;

    // M�todo Awake que se llama al iniciar el script
    void Awake ()
    {
        // Obtiene el componente ConstantRotation del objeto
        constantRotation = GetComponent< ConstantRotation >();
    }

    // M�todo Update que se llama una vez por frame
    void Update ()
    {
        // Obtiene el �ngulo actual de ConstantRotation
        float angle = constantRotation.GetAngle();

        // Reinicia el temporizador si el �ngulo est� entre -45 y 45
        bool isBelowThreshold = angle < 45f && angle > -45f;

        if ( isBelowThreshold )
        {
            timer = 0f;
            return;
        }

        // Incrementa el temporizador mientras si se pasa del �ngulo de derrota
        timer += Time.deltaTime;

        // Si el temporizador excede el intervalo de derrota, termina la rotaci�n
        bool isAboveThreshold = timer > defeatInterval;

        if ( isAboveThreshold )
        {
            constantRotation.MakeFinish();
            timer = 0f;
        }
    }
}
