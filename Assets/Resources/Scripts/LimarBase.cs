using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase LimarBase que hereda de MonoBehaviour e implementa la interfaz IPointerEnterHandler
public class LimarBase : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private ActivarBotonLimar boton; // Referencia al script BotonGirar
    private CanvasGroup canvasGroup; // Referencia al componente CanvasGroup
    private int veces; // Contador de veces que se ha pasado la lima por encima
    private const int maxVeces = 4; // Número máximo de veces que se puede pasar la lima por encima

    // Método Start que se ejecuta al inicio
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Método que se ejecuta cuando un objeto pasa por encima de este componente
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Verifica si el objeto arrastrado no es nulo y si este objeto está activo
        bool isDragged = eventData.pointerDrag != null && gameObject.activeSelf;

        if (isDragged)
        {
            // Incrementa el contador de veces
            veces++;

            // Reduce la opacidad del objeto
            float newAlpha = Mathf.Clamp01(1f - (veces / (float)maxVeces));
            canvasGroup.alpha = newAlpha;

            //Reproducir sonido de limar esmalte
            AudioManager.instance.PlaySFX( AudioManager.instance.scratchBase );

            // Si se ha pasado el objeto el número máximo de veces, desactiva el objeto
            if (veces >= maxVeces)
            {
                gameObject.SetActive(false);

                // Llama al método RestarContador del script del botón girar
                if (boton != null)
                {
                    boton.RestarContador();
                }
            }
        }
    }
}