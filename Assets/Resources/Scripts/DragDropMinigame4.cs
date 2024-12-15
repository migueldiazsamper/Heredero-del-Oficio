using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

// Definición de la clase DragDropMinigame4 que hereda de MonoBehaviour e implementa varias interfaces de eventos de Unity
public class DragDropMinigame4 : MonoBehaviour, IBeginDragHandler , IEndDragHandler , IDragHandler
{
    // Referencia al componente Canvas, asignada desde el inspector de Unity
    [ SerializeField ] private Canvas canvas;
    //Referencia a un objeto que tenga el script ItemSlot, se usa para saber si el objeto está en un slot y en cual
    public ItemSlot currentSlot;
    private ItemSlot lastSlot;
    // Referencias a los componentes RectTransform, CanvasGroup y CombustibleHorno del objeto
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private CombustibleHorno combustibleHorno;
    [SerializeField] WoodHandler woodHandler;

    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena los componentes RectTransform, CanvasGroup y CombustibleHorno del objeto
        rectTransform = GetComponent< RectTransform >();
        canvasGroup = GetComponent< CanvasGroup >();
        combustibleHorno = GetComponent<CombustibleHorno>();
        woodHandler = FindFirstObjectByType<WoodHandler>();
    }

    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {   
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if(combustibleHorno.IsDraggingAllowed() && !CountDownTimer.instance.GetisTimeUp())
        {
            // Reproducir sonido coger madera
            AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().grabWood);

            // Reduce la opacidad del objeto
            canvasGroup.alpha = .6f;
            // Permite que el objeto sea atravesado por rayos de detección
            canvasGroup.blocksRaycasts = false;
        }
    }

    // Método que se llama mientras se arrastra el objeto
    public void OnDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //Le pedimos al script de las mecánicas si está permitido mover el objeto
        if(combustibleHorno.IsDraggingAllowed() && !CountDownTimer.instance.GetisTimeUp())
        {
            // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    // Método que se llama al finalizar el arrastre del objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        if(combustibleHorno.IsDraggingAllowed() && !CountDownTimer.instance.GetisTimeUp())
        {
            // Restaura la opacidad del objeto
            canvasGroup.alpha = 1f;
            // Impide que el objeto sea atravesado por rayos de detección
            canvasGroup.blocksRaycasts = true;

            // Verifica si el objeto se soltó sobre un slot o la basura
            if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<RectTransform>() != null)
            {
                // Comprueba si existe el componente del script asociado al slot y lo obtiene en caso positivo
                ItemSlot slotScript = eventData.pointerEnter.GetComponent<ItemSlot>();

                if (slotScript != null && !combustibleHorno.IsBurnt())
                {
                    if (slotScript.freeOfItem)
                    {
                        // Coloca el objeto en el centro del slot
                        RectTransform slotRectTransform = eventData.pointerEnter.GetComponent<RectTransform>();
                        rectTransform.position = slotRectTransform.position;
                        slotScript.freeOfItem = false; // Marca el slot como ocupado
                        combustibleHorno.StartBurning(); // Comienza el quemado de la madera
                        slotScript.currentMaderita = combustibleHorno;
                        woodHandler.RemoveMaderitaFromDeck(gameObject); //Marcamos que la maderita ya no está en la baraja
                        woodHandler.AddMaderitaToDeck(); //Añadimos una nueva maderita al slot vacío de la baraja
                    }
                }

                // Si no es un slot, comprueba si el objeto es la basura
                else if(eventData.pointerEnter.CompareTag("Trash"))
                {   
                    // Reproducir sonido tirar madera a la basura
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().dropWood);

                    if(combustibleHorno.IsBurnt())
                    {
                        currentSlot.freeOfItem = true; // Marca el slot como libre
                        currentSlot = null; // El objeto ya no está en ningún slot
                        woodHandler.ResetMaderita(this, false); //False porque la maderita no viene del deck
                    }
                    else woodHandler.ResetMaderita(this, true); //Si se posa una madera que viene del deck en la basura, se resetea normal

                }
                else{
                    if(!combustibleHorno.IsBurnt()) woodHandler.ResetMaderita(this, true);
                    else rectTransform.position = currentSlot.GetComponent<RectTransform>().position;
                    }

            }
            
            //Si no se soltó sobre slot o basura, comprueba si está quemado, si lo está, devuélvelo a donde estaba
            else if(combustibleHorno.IsBurnt())
            {
                rectTransform.position = currentSlot.GetComponent<RectTransform>().position;
            }

            else
            {
                woodHandler.ResetMaderita(this, true); //True porque la maderita sí viene del deck
            }
        }
    }

    public void SetCurrentMaderitaAsNull()
    {
        currentSlot.GetComponent<ItemSlot>().currentMaderita = null;
    }
}