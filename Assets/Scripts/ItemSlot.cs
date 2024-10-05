using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase ItemSlot que hereda de MonoBehaviour e implementa la interfaz IDropHandler
public class ItemSlot : MonoBehaviour , IDropHandler
{

    // Método que se ejecuta cuando un objeto es soltado en el área de este componente
    public void OnDrop( PointerEventData eventData ) 
    {
        // Verifica si el objeto arrastrado no es nulo
        bool isDragged = eventData.pointerDrag != null;

        if ( isDragged ) 
        {
            // Obtiene el componente RectTransform del objeto arrastrado y lo posiciona en la misma posición anclada que este componente
            eventData.pointerDrag.GetComponent< RectTransform >().anchoredPosition = GetComponent< RectTransform >().anchoredPosition;
        }
    }
}