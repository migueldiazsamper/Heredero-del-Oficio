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

    private Transform slotsParent;
    private Transform piecesParent;
    private ItemSlot currentSlot;

    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena el componente RectTransform del objeto
        rectTransform = GetComponent< RectTransform >();
        // Obtiene y almacena el componente CanvasGroup del objeto
        canvasGroup = GetComponent< CanvasGroup >();
        // Encuentra los padres de los slots y las piezas
        slotsParent = GameObject.Find( "Slots" ).transform;
        piecesParent = GameObject.Find( "Piezas" ).transform;
    }

    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        // Reduce la opacidad del objeto
        canvasGroup.alpha = .6f;
        // Permite que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = false;

        if (currentSlot != null) currentSlot.freeOfItem = true;
    }

    // Método que se llama mientras se arrastra el objeto
    public void OnDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Cambia el orden de los padres para renderizar las piezas por debajo de los slots
        slotsParent.SetSiblingIndex( piecesParent.GetSiblingIndex() + 1 );
    }

    // Método que se llama al finalizar el arrastre del objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;

        // Cambia el orden de los padres para renderizar las piezas por encima de los slots
        piecesParent.SetSiblingIndex( slotsParent.GetSiblingIndex() + 1 );

        // Verifica si el objeto se soltó sobre un slot
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<RectTransform>() != null)
        {
            // Obtiene el componente del script asociado al slot
            currentSlot = eventData.pointerEnter.GetComponent<ItemSlot>();

            if (currentSlot != null)
            {
                if (currentSlot.freeOfItem)
                {
                    // Coloca el objeto en el centro del slot
                    RectTransform slotRectTransform = eventData.pointerEnter.GetComponent<RectTransform>();
                    rectTransform.position = slotRectTransform.position;
                    currentSlot.freeOfItem = false; // Marca el slot como ocupado
                }
                else
                {
                    // Genera una posición aleatoria en la pantalla
                    float randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
                    float randomY = Random.Range(-Screen.height / 2, Screen.height / 2);

                    // Asigna la nueva posición aleatoria al objeto arrastrado
                    rectTransform.anchoredPosition = new Vector2(randomX, randomY);
                }
            }
        }
    }

    // Método que se llama al presionar el puntero sobre el objeto
    public void OnPointerDown ( PointerEventData eventData )
    {
        // Este método está vacío, pero es necesario para implementar la interfaz IPointerDownHandler
    }
}