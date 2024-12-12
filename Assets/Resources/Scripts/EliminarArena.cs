using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EliminarArena : MonoBehaviour , IPointerClickHandler
{
    // Duración de la desaparición en segundos
    public float fadeDuration = 0.5f;

    // Lista de objetos a desvanecer y eliminar
    public List< GameObject > objectsToFadeAndDestroy;

    // Referencia al componente Image
    private Image image;
    private bool isFading = false;

    private void Start ()
    {
        // Obtiene la referencia al componente Image
        image = GetComponent< Image >();
        if ( image == null )
        {
            Debug.LogError( "Image no encontrado en el objeto " + gameObject.name );
        }
    }

    // Método que se llama al hacer clic en el objeto
    public void OnPointerClick ( PointerEventData eventData )
    {
        // Inicia la corrutina para desaparecer el objeto y los objetos especificados
        if(!isFading) StartCoroutine( FadeOutAndDestroy() );
    }

    private IEnumerator FadeOutAndDestroy ()
    {
        isFading = true;

        // Reproducir sonido quitar arena
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().grabSand);

        // Desvanece y destruye el objeto actual
        yield return StartCoroutine( FadeOut( image ) );

        // Desvanece y destruye los objetos especificados
        foreach ( GameObject obj in objectsToFadeAndDestroy )
        {
            Image objImage = obj.GetComponent< Image >();
            if ( objImage != null )
            {
                yield return StartCoroutine( FadeOut( objImage ) );
                Destroy( obj );
            }
        }

        // Destruye el objeto actual
        Destroy( gameObject );
    }

    private IEnumerator FadeOut ( Image img )
    {
        // Tiempo inicial
        float startTime = Time.time;

        // Opacidad inicial
        Color initialColor = img.color;

        while ( Time.time < startTime + fadeDuration )
        {
            // Calcula el tiempo transcurrido
            float elapsed = Time.time - startTime;

            // Calcula la nueva opacidad
            float alpha = Mathf.Lerp( 1.0f , 0.0f , elapsed / fadeDuration );

            // Aplica la nueva opacidad al color del Image
            img.color = new Color( initialColor.r , initialColor.g , initialColor.b , alpha );

            // Espera al siguiente frame
            yield return null;
        }

        // Asegura que la opacidad sea 0
        img.color = new Color( initialColor.r , initialColor.g , initialColor.b , 0.0f );
    }
}