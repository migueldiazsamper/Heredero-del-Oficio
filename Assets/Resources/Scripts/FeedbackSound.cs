using System.Collections;
using UnityEngine;

// Definición de la clase FeedbackSound que hereda de MonoBehaviour
public class FeedbackSound : MonoBehaviour
{
    // Referencia al componente AudioSource
    private AudioSource audiosource;

    // Referencia al objeto hijo (Sprite que aparece un pantalla)
    public GameObject childSprite;

    // Duración de la activación del sprite
    public float activationDuration = 2.0f;

    // Corrutina para activar el sprite por un tiempo determinado
    IEnumerator ActivateChild ()
    {
        // Activa el sprite
        childSprite.SetActive( true );

        // Espera el tiempo de duración
        yield return new WaitForSeconds( activationDuration );

        // Desactiva el sprite
        childSprite.SetActive( false );
    }

    // Método Start que se llama al iniciar el script
    void Start ()
    {
        // Obtiene el componente AudioSource
        audiosource = GetComponent < AudioSource >();

        // Desactiva el sprite hijo al iniciar
        bool spriteExists = childSprite != null;

        if ( spriteExists )
        {
            childSprite.SetActive( false );
        }
    }

    // Método Update que se llama una vez por frame
    void Update ()
    {
        // Si se presiona la tecla "X", reproduce el sonido y activa el sprite (Cambiar para trigger correspondiente)
        bool triggerIsActive = Input.GetKeyDown( KeyCode.X );

        if ( triggerIsActive )
        {
            // Comprueba que existe un clip en AudioSource y lo reproduce
            bool audiosourceClipExists = audiosource != null && audiosource.clip != null;

            if ( audiosourceClipExists ) 
            {
                audiosource.Play();
            }

            // Comprueba que existe un sprite hijo y lo activa mediante la corrutina ActivateChild
            bool spriteExists = childSprite != null;

            if ( spriteExists )
            {
                StartCoroutine( ActivateChild() );
            }
        }
    }
}
