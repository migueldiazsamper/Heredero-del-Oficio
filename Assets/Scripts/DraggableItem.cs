using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public ItemSlot currentSlot; // Referencia al slot actual en el que se encuentra el objeto

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private int currentRotation = 0; // Rotación actual del objeto
    [ SerializeField ] private int correctRotation = 3;
    int initialRotation;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        initialRotation = Random.Range(0, 4);
        currentRotation = initialRotation;
        rectTransform.Rotate(0, 0, initialRotation * 90);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        // Si el objeto está en un slot, marca el slot como libre
        if (currentSlot != null)
        {
            currentSlot.freeOfItem = true;
            currentSlot = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Si el objeto no se soltó sobre un slot, genera una posición aleatoria en la pantalla
        if (currentSlot == null)
        {
            float randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
            float randomY = Random.Range(-Screen.height / 2, Screen.height / 2);
            rectTransform.anchoredPosition = new Vector2(randomX, randomY);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Si el objeto está en un slot, rota 90º en sentido horario
        if (currentSlot != null)
        {
            rectTransform.Rotate(0, 0, -90);
            if ( ++currentRotation > 3 )
            {
                currentRotation = 0;
            }

            switch ( currentRotation )
            {
                case 0:
                    rectTransform.Rotate(0, 0, 0);
                    break;
                case 1:
                    rectTransform.Rotate(0, 0, 90);
            }

            if ( currentRotation == correctRotation )
            {
                Debug.Log("Correcto");
            }
        }
    }
}