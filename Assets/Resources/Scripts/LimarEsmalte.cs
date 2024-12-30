using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase LimarEsmalte que hereda de MonoBehaviour e implementa la interfaz IDropHandler
public class LimarEsmalte : MonoBehaviour , IDropHandler
{
    [SerializeField] private ActivarBotonLimar boton; // Referencia al script BotonGirar

    // Método que se ejecuta cuando un objeto es soltado en el área de este componente
    public void OnDrop ( PointerEventData eventData ) 
    {
        // Verifica si el objeto arrastrado no es nulo
        bool isDragged = eventData.pointerDrag != null && gameObject.activeSelf;
        
        if ( isDragged ) 
        {   
            // Desactiva el objeto que tiene este script
            gameObject.SetActive ( false );

            //Reproducir sonido de limar esmalte
            AudioManager.GetInstance().PlaySFX( AudioManager.GetInstance().scratchEnamel, AudioManager.GetInstance().scratchEnamelVolume);

            // Resta 1 al contador del script del botón girar
            if (boton != null)
            {
                boton.RestarContador();
            }
        }
    }

}