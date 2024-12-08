using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSound : MonoBehaviour
{

    public AudioSource characterMovementSource;

    public static CharacterMovementSound instance;

    public static CharacterMovementSound GetInstance ()
    {
        return instance;
    }

    private void Awake ()
    {
        // Comprueba si ya existe una instancia de AudioManager.
        bool noExisteInstancia = instance == null;

        if ( noExisteInstancia )
        {
            instance = this;

            // Hace que este GameObject persista entre escenas.
            DontDestroyOnLoad( gameObject );
        }
        else
        {
            // Si ya hay una instancia, destruye este GameObject duplicado.
            Destroy( gameObject );
        }
    }

    public void PlayCharacterMovement ()
    {
        if (!characterMovementSource.isPlaying)
        {
            characterMovementSource.Play();
        }
    }
    
    public void StopCharacterMovement()
    {
        if (characterMovementSource.isPlaying)
        {
            characterMovementSource.Stop();
        }
    }
}
