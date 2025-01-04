using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterMovementSound : MonoBehaviour
{

    public AudioSource characterMovementSource;
    public Slider slider;

    public static CharacterMovementSound instance;

    public static CharacterMovementSound GetInstance ()
    {
        return instance;
    }

    private void Awake ()
    {
        instance = this;
    }

    private void Start ()
    {
        characterMovementSource.volume = slider.value * 0.4f;
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
