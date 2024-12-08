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
        instance = this;
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
