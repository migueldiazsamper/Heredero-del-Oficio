using UnityEngine;
using UnityEngine.EventSystems;

// Clase que representa la herramienta para mezclar colores
public class MixTool : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Referencia al PigmentosManager
    private PigmentosManager manager;

    // Referencia al RectTransform del objeto
    private RectTransform rectTransform;
    // Referencia al CanvasGroup del objeto
    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Obtiene la referencia al PigmentosManager
        manager = FindObjectOfType<PigmentosManager>();
        // Obtiene el RectTransform del objeto
        rectTransform = GetComponent<RectTransform>();
        // Obtiene el CanvasGroup del objeto
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Método que se llama al comenzar a arrastrar la herramienta
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Permite que el objeto sea arrastrado
        canvasGroup.alpha = 0.6f; // Reduce la opacidad para indicar que está siendo arrastrado
        canvasGroup.blocksRaycasts = false; // Permite que el objeto sea atravesado por rayos de detección
    }

    // Método que se llama mientras se arrastra la herramienta
    public void OnDrag(PointerEventData eventData)
    {
        // Actualiza la posición del objeto basado en el movimiento del puntero
        rectTransform.anchoredPosition += eventData.delta;
    }

    // Método que se llama al finalizar el arrastre de la herramienta
    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;

        // Verifica si la herramienta se soltó en el trigger
        if (RectTransformUtility.RectangleContainsScreenPoint(manager.mixTrigger, Input.mousePosition))
        {
            // Llama al método para mezclar colores
            manager.MixColors();
        }
    }
}