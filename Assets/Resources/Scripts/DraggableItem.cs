using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Esta clase permite que un objeto sea arrastrable y clicable en la interfaz de usuario de Unity
public class DraggableItem : MonoBehaviour , IBeginDragHandler , IEndDragHandler , IDragHandler , IPointerClickHandler
{
    public ItemSlot currentSlot; // Referencia al slot actual en el que se encuentra el objeto
    public CheckCompletionMinigame2 checkCompletionMinigame2; // Referencia al script que comprueba la finalización del minijuego

    private RectTransform rectTransform; // Componente que controla la posición, tamaño y rotación del objeto
    private CanvasGroup canvasGroup; // Componente que controla la transparencia y la capacidad de recibir eventos del objeto
    private Canvas canvas; // Referencia al canvas (lienzo) en el que se encuentra el objeto

    public int currentRotation = 0; // Rotación actual del objeto
    [SerializeField] private int[] correctSlots; // Slots correctos para cada rotación, para cuando la rotacion es [0, -90, 180, 90]
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
        checkCompletionMinigame2 = FindObjectOfType<CheckCompletionMinigame2>();
    }



    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {
        // Reproducir sonido coger pieza
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().grabPiece, AudioManager.GetInstance().grabPieceVolume);

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
            // Convierte la posición del ratón a la posición anclada en el espacio del rectángulo del canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector2 localPoint);
            rectTransform.anchoredPosition = localPoint;
        }
        else
        {
            // Reproducir sonido soltar pieza
            AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().dropPiece, AudioManager.GetInstance().dropPieceVolume);

            // Verifica si el slot es uno de los corretos y si la rotación de la pieza es correcta para ese slot
            bool isCorrectSlotAndRotation = currentSlot.slotID == correctSlots[currentRotation];

            if ( isCorrectSlotAndRotation )
            {
                // Marca la rotación actual como correcta en el script de comprobación si no hay rotación correcta
                if(checkCompletionMinigame2.correctRotation == -1) {
                    checkCompletionMinigame2.correctRotation = currentRotation;
                    // Reproducir sonido correcto
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback, AudioManager.GetInstance().positiveFeedbackVolume);
                }
                else if(checkCompletionMinigame2.correctRotation == currentRotation){ //Si la rotación es correcta
                    // Reproducir sonido correcto
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback, AudioManager.GetInstance().positiveFeedbackVolume);
                }
                
            }   
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

            // Reproducir sonido rotar
            AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().rotatePiece, AudioManager.GetInstance().rotatePieceVolume);

            // Incrementa la rotación actual en 90 grados
            if ( ++currentRotation > 3 )
            {
                currentRotation = 0; // Reinicia la rotación si supera los 360 grados
            }

            // Verifica si el slot es uno de los corretos y si la rotación de la pieza es correcta para ese slot
            bool isCorrect = currentSlot.slotID == correctSlots[currentRotation];
            // Verifica si es la pieza central
            bool isCenterPiece = correctSlots[0] == 5 && correctSlots[1] == 5 && correctSlots[2] == 5 && correctSlots[3] == 5;

            if ( isCorrect )
            {
                // Marca la rotación actual como correcta en el script de comprobación si no hay rotación correcta
                if(checkCompletionMinigame2.correctRotation == -1) {
                    checkCompletionMinigame2.correctRotation = currentRotation;
                    // Reproducir sonido correcto
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback, AudioManager.GetInstance().positiveFeedbackVolume);
                }
                else if(checkCompletionMinigame2.correctRotation == currentRotation){ //Si la rotación es correcta
                    // Reproducir sonido correcto
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback, AudioManager.GetInstance().positiveFeedbackVolume);
                }
            }   
        }
    }
}