using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase DragDrop que hereda de MonoBehaviour e implementa varias interfaces de eventos de Unity
public class DragDrop : MonoBehaviour , IPointerDownHandler , IBeginDragHandler , IEndDragHandler , IDragHandler
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
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        // Reduce la opacidad del objeto
        canvasGroup.alpha = .6f;
        // Permite que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = false;
        // Coloca el objeto en un ángulo de 45º
        rectTransform.rotation = Quaternion.Euler( 0 , 0 , 45 );
    }

    // Método que se llama mientras se arrastra el objeto
    public void OnDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Método que se llama al finalizar el arrastre del objeto
    public void OnEndDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;
        // Coloca el objeto en un ángulo de 90º
        rectTransform.rotation = Quaternion.Euler( 0 , 0 , 90 );
    }

    // Método que se llama al presionar el puntero sobre el objeto
    public void OnPointerDown ( PointerEventData eventData )
    {
        // Este método está vacío, pero es necesario para implementar la interfaz IPointerDownHandler
    }
}