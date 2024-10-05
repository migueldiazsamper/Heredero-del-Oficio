using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase DragDropMinigame2 que hereda de MonoBehaviour e implementa varias interfaces de eventos de Unity
public class DragDropMinigame2 : MonoBehaviour , IPointerDownHandler , IBeginDragHandler , IEndDragHandler , IDragHandler
{
    // Referencia al componente Canvas, asignada desde el inspector de Unity
    [ SerializeField ] private Canvas canvas;

    // Referencias a los componentes RectTransform y CanvasGroup del objeto
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena el componente RectTransform del objeto
        rectTransform = GetComponent< RectTransform >();
        // Obtiene y almacena el componente CanvasGroup del objeto
        canvasGroup = GetComponent< CanvasGroup >();
    }

    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {
        // Reduce la opacidad del objeto
        canvasGroup.alpha = .6f;
        // Permite que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = false;
    }

    // Método que se llama mientras se arrastra el objeto
    public void OnDrag ( PointerEventData eventData )
    {
        // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Método que se llama al finalizar el arrastre del objeto
    public void OnEndDrag ( PointerEventData eventData )
    {
        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;

        // Coloca el objeto en el centro del slot
        bool isNotCentered = eventData.pointerEnter != null && eventData.pointerEnter.GetComponent< RectTransform >() != null;
        
        if ( isNotCentered )
        {
            RectTransform slotRectTransform = eventData.pointerEnter.GetComponent< RectTransform >();
            // Ajusta la posición del objeto arrastrado para que esté en el centro del slot
            rectTransform.position = slotRectTransform.position;
        }
    }

    // Método que se llama al presionar el puntero sobre el objeto
    public void OnPointerDown ( PointerEventData eventData )
    {
        // Este método está vacío, pero es necesario para implementar la interfaz IPointerDownHandler
    }
}