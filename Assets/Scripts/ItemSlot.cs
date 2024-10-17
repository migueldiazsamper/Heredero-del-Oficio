using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase ItemSlot que hereda de MonoBehaviour e implementa la interfaz IDropHandler
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool freeOfItem = true; // Indicador de si el área de este componente está libre de objetos

    // Método que se ejecuta cuando un objeto es soltado en el área de este componente
    public void OnDrop(PointerEventData eventData)
    {
        // Verifica si el objeto arrastrado no es nulo
        bool isDragged = eventData.pointerDrag != null;

        if (isDragged)
        {
            if (freeOfItem)
            {
                // Obtiene el componente RectTransform del objeto arrastrado y lo posiciona en la misma posición anclada que este componente
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                StartCoroutine(SetFreeOfItemFalseWithDelay()); // Inicia la corrutina para cambiar freeOfItem a false después de medio segundo
                //Comprueba si el objeto arrastrado tiene un script DraggableItem. Si no lo tiene, usa el del minijuego 4 
                bool isDraggableItem = eventData.pointerDrag.GetComponent<DraggableItem>() != null;
                if(isDraggableItem)
                {
                    eventData.pointerDrag.GetComponent<DraggableItem>().currentSlot = this; // Asigna este slot como el slot actual del objeto arrastrado
                }
                else
                {
                    eventData.pointerDrag.GetComponent<DragDropMinigame4>().currentSlot = this;
                }
            }   
        }
    }

    // Corrutina que espera medio segundo antes de cambiar freeOfItem a false
    private IEnumerator SetFreeOfItemFalseWithDelay()
    {
        yield return new WaitForSeconds(0.15f);
        freeOfItem = false; // Marca el área de este componente como ocupada
    }
}