using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que verifica si se cumple la condición de derrota basada en la rotación de un objeto.
/// Detiene la rotación y el temporizador cuando se supera el umbral permitido.
/// </summary>

public class CheckIfDefeat : MonoBehaviour
{
    // Referencia al componente ConstantRotation en el mismo objeto.
    private ConstantRotation constantRotation;

    // Referencia pública al objeto que actúa como referencia de rotación.
    public GameObject rotationReference;

    // Referencia pública al componente CountDownTimer.
    public CountDownTimer countDownTimer;

    // Botón que se activa cuando se alcanza la condición de derrota.
    [ SerializeField ]
    private GameObject listoButton;

    /// <summary>
    /// Método Awake: inicializa referencias y configura el estado inicial del botón.
    /// </summary>

    private void Awake ()
    {
        // Obtiene el componente ConstantRotation del mismo GameObject.
        constantRotation = GetComponent< ConstantRotation >();

        // Asegura que el botón esté desactivado al inicio.
        listoButton.SetActive( false );
    }

    /// <summary>
    /// Método Update: verifica continuamente si la rotación del objeto de referencia excede el umbral permitido.
    /// </summary>

    private void Update ()
    {
        // Comprueba si existe el objeto de referencia de rotación.
        bool referenciaDeRotacionExiste = rotationReference != null;

        if ( referenciaDeRotacionExiste )
        {
            // Obtiene el ángulo de rotación en el eje Z.
            float rotationZ = rotationReference.transform.rotation.eulerAngles.z;

            // Normaliza el valor de rotationZ al rango [-180, 180].
            if ( rotationZ > 180f )
            {
                rotationZ -= 360f;
            }

            // Verifica si la rotación está dentro del rango permitido (-45 a 45 grados).
            bool dentroDeUmbral = rotationZ > -45f && rotationZ < 45f;

            if ( !dentroDeUmbral )
            {
                // Detiene la rotación del objeto llamando al método MakeFinish del componente ConstantRotation.
                constantRotation.MakeFinish();

                // Si el temporizador existe, marca el tiempo como agotado.
                bool temporizadorExiste = countDownTimer != null;

                if ( temporizadorExiste && !countDownTimer.GetisDefeat() )
                {
                    countDownTimer.SetDefeat();
                }

                // Activa el botón para indicar el estado de derrota.
                listoButton.SetActive( true );
            }
        }
    }
}
