using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    // Referencia al objeto del que obtiene el ángulo de rotación
    public GameObject objectToMatch;

    // Referencia al componente llamado ConstantRotation que está en el objeto del cual obtiene el ángulo de rotación
    private ConstantRotation constantRotation;

    // Referencia al componente que almacena el ángulo de constantRotation
    private float angle;


    // Este método se llama una vez al inicio del juego
    void Start()
    {
        if (objectToMatch != null)
        {
            constantRotation = objectToMatch.GetComponent<ConstantRotation>();
        }
    }

    // Este método se llama una vez por cada frame (fotograma) del juego
    void Update()
    {
        if (constantRotation != null)
        {
            angle = constantRotation.GetAngle();

            // Rotación deseada del GameObject
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Interpolación suave de la rotación del GameObject usando Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * constantRotation.GetDifficulty());
        }
    }
}
