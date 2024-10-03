using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Definición de la clase ItemEliminate que hereda de MonoBehaviour e implementa la interfaz IDropHandler
public class ItemEliminate : MonoBehaviour, IDropHandler
{
    // Referencia al GameObject en el que se posicionará este objeto
    [SerializeField] private GameObject referenceGameObject;

    // Método que se ejecuta cuando un objeto es soltado en el área de este componente
    public void OnDrop(PointerEventData eventData) 
    {
        // Verifica si el objeto arrastrado no es nulo
        bool isDragged = eventData.pointerDrag != null;
        if (isDragged) 
        {
            // Posiciona este objeto en la misma posición anclada que el referenceGameObject
            GetComponent<RectTransform>().anchoredPosition = referenceGameObject.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    // Método que se ejecuta cuando este objeto colisiona con otro
    private void OnCollisionEnter(Collision collision)
    {
        // Obtiene el componente RectTransform del objeto con el que colisionamos
        RectTransform collidedRectTransform = collision.gameObject.GetComponent<RectTransform>();
        
        // Verifica si el objeto colisionado tiene un componente RectTransform
        if (collidedRectTransform != null)
        {
            // Posiciona este objeto en la misma posición anclada que el objeto colisionado
            GetComponent<RectTransform>().anchoredPosition = collidedRectTransform.anchoredPosition;
        }
    }
}