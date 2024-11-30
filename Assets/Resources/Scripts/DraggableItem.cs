using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Esta clase permite que un objeto sea arrastrable y clicable en la interfaz de usuario de Unity
public class DraggableItem : MonoBehaviour , IBeginDragHandler , IEndDragHandler , IDragHandler , IPointerClickHandler
{
    public ItemSlot currentSlot; // Referencia al slot actual en el que se encuentra el objeto

    private RectTransform rectTransform; // Componente que controla la posición, tamaño y rotación del objeto
    private CanvasGroup canvasGroup; // Componente que controla la transparencia y la capacidad de recibir eventos del objeto
    private Canvas canvas; // Referencia al canvas (lienzo) en el que se encuentra el objeto

    private int currentRotation = 0; // Rotación actual del objeto
    [ SerializeField ] private int correctRotation = 0; // Rotación correcta que debe tener el objeto
    int initialRotation = 0; // Rotación inicial aleatoria del objeto

    // Método que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena los componentes necesarios del objeto
        rectTransform = GetComponent< RectTransform >();
        canvasGroup = GetComponent< CanvasGroup >();
        canvas = GetComponentInParent< Canvas >();
        
        // Asigna una rotación inicial aleatoria al objeto (1, 2 o 3)
        initialRotation = Random.Range( 1 , 4 );
        currentRotation = initialRotation;

        // Aplica la rotación inicial al objeto según el valor aleatorio generado
        switch ( initialRotation )
        {
            case 1:
                rectTransform.rotation = Quaternion.Euler( 0 , 0 , -90 );  // Esto rota el objeto 90 grados en el eje Z
                break;
            case 2:
                rectTransform.rotation = Quaternion.Euler( 0 , 0 , 180 );  // Esto rota el objeto 180 grados en el eje Z
                break;
            case 3:
                rectTransform.rotation = Quaternion.Euler( 0 , 0 , 90 );  // Esto rota el objeto 270 grados en el eje Z
                break;
            default:
                break;
        }
    }

    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {
        // Reduce la opacidad del objeto para indicar que está siendo arrastrado
        canvasGroup.alpha = .6f;
        // Permite que el objeto sea atravesado por rayos de detección (para que no interfiera con otros objetos)
        canvasGroup.blocksRaycasts = false;

        // Si el objeto está en un slot, marca el slot como libre
        bool wasInSlot = currentSlot != null;

        if ( wasInSlot )
        {
            currentSlot.freeOfItem = true; // Marca el slot como libre
            currentSlot = null; // El objeto ya no está en ningún slot
        }
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

        // Si el objeto no se soltó sobre un slot, genera una posición aleatoria en la pantalla
        bool notDroppedOnSlot = currentSlot == null;

        if ( notDroppedOnSlot )
        {
            // Genera una posición aleatoria en la pantalla
            float randomX = Random.Range( -Screen.width / 2 , Screen.width / 2 );
            float randomY = Random.Range( -Screen.height / 2 , Screen.height / 2 );
            rectTransform.anchoredPosition = new Vector2( randomX , randomY );
        }
    }

    // Método que se llama al hacer clic en el objeto
    public void OnPointerClick ( PointerEventData eventData )
    {
        // Si el objeto está en un slot, rota 90º en sentido horario
        bool isOnSlot = currentSlot != null;

        if ( isOnSlot )
        {
            // Rota el objeto 90 grados en el eje Z
            rectTransform.Rotate( 0 , 0 , -90 );

            // Incrementa la rotación actual en 90 grados
            if ( ++currentRotation > 3 )
            {
                currentRotation = 0; // Reinicia la rotación si supera los 360 grados
            }

            // Verifica si la rotación actual es la correcta
            bool isCorrect = currentRotation == correctRotation;

            if ( isCorrect )
            {
                Debug.Log( "Correcto" ); // Imprime un mensaje en la consola si la rotación es correcta
            }   
        }
    }
}