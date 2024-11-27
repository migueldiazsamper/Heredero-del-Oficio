using UnityEngine;
using UnityEngine.UI;

// Esta clase se llama CheckIfDefeat y se encarga de verificar si se ha alcanzado una condición de derrota en el juego
public class CheckIfDefeat : MonoBehaviour
{
    // Referencia a otro componente llamado ConstantRotation que está en el mismo objeto
    private ConstantRotation constantRotation;

    // Referencia pública al objeto de referencia de rotación
    public GameObject rotationReference;

    // Referencia pública al componente CountDownTimer
    public CountDownTimer countDownTimer;

    [SerializeField] private GameObject listoButton;


    // Este método se llama una vez cuando el script se inicializa
    void Awake ()
    {
        // Obtiene el componente ConstantRotation del mismo objeto y lo guarda en la variable constantRotation
        constantRotation = GetComponent< ConstantRotation >();
        listoButton.SetActive(false);
    }

    // Este método se llama una vez por cada frame (fotograma) del juego
    void Update()
    {
        if (rotationReference != null)
        {
            // Obtiene la rotación "z" del transform del objeto de referencia de rotación
            float rotationZ = rotationReference.transform.rotation.eulerAngles.z;

            // Ajusta el valor de rotationZ para que esté en el rango [-180, 180]
            if (rotationZ > 180f)
            {
                rotationZ -= 360f;
            }

            // Verifica si la rotación "z" está entre -45 y 45 grados
            bool isBelowThreshold = rotationZ < 45f && rotationZ > -45f;

            if (!isBelowThreshold)
            {
                // Si se ha superado el intervalo, llama al método MakeFinish del componente ConstantRotation para detener la rotación
                constantRotation.MakeFinish();

                // Detiene el temporizador en CountDownTimer
                if (countDownTimer != null)
                {
                    countDownTimer.SetisTimeUp(true);
                }

                listoButton.SetActive(true);
            }
        }
    }
}