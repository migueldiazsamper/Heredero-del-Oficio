using UnityEngine;

// Esta clase se llama CheckIfDefeat y se encarga de verificar si se ha alcanzado una condición de derrota en el juego
public class CheckIfDefeat : MonoBehaviour
{
    // Referencia a otro componente llamado ConstantRotation que está en el mismo objeto
    private ConstantRotation constantRotation;

    // Tiempo en segundos que debe pasar para considerar una derrota (10 segundos)
    [ SerializeField ] private float defeatInterval = 10f;
    // Temporizador que cuenta el tiempo transcurrido
    private float timer;

    // Este método se llama una vez cuando el script se inicializa
    void Awake ()
    {
        // Obtiene el componente ConstantRotation del mismo objeto y lo guarda en la variable constantRotation
        constantRotation = GetComponent< ConstantRotation >();
    }

    // Este método se llama una vez por cada frame (fotograma) del juego
    void Update ()
    {
        // Obtiene el ángulo actual de rotación del componente ConstantRotation
        float angle = constantRotation.GetAngle();

        // Verifica si el ángulo está entre -45 y 45 grados
        bool isBelowThreshold = angle < 45f && angle > -45f;

        if ( isBelowThreshold )
        {
            // Si el ángulo está dentro del rango, reinicia el temporizador a 0
            timer = 0f;
            // Sale del método Update y espera al siguiente frame
            return;
        }

        // Si el ángulo está fuera del rango, incrementa el temporizador con el tiempo transcurrido desde el último frame
        timer += Time.deltaTime;

        // Verifica si el temporizador ha superado el intervalo de derrota (10 segundos)
        bool isAboveThreshold = timer > defeatInterval;

        if ( isAboveThreshold )
        {
            // Si el temporizador ha superado el intervalo, llama al método MakeFinish del componente ConstantRotation para detener la rotación
            constantRotation.MakeFinish();
            // Reinicia el temporizador a 0
            timer = 0f;
        }
    }
}