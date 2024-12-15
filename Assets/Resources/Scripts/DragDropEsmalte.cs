using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropEsmalte : MonoBehaviour , IBeginDragHandler , IEndDragHandler , IDragHandler
{
    // Referencia al componente Canvas, asignada desde el inspector de Unity
    [ SerializeField ] private Canvas canvas;

    // Referencias a los componentes RectTransform y CanvasGroup del objeto
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition; 
    [SerializeField] GameObject piezaEsmaltada;
    [SerializeField] bool isEsmaltada;
    [SerializeField] GameObject botonListo;
    

    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena el componente RectTransform del objeto
        rectTransform = GetComponent< RectTransform >();
        // Obtiene y almacena el componente CanvasGroup del objeto
        canvasGroup = GetComponent< CanvasGroup >();
        this.enabled = false;
    }

    void Start(){
        initialPosition = rectTransform.anchoredPosition;
    }

    // Método que se llama al comenzar a arrastrar el objeto
    public void OnBeginDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Reproducir sonido de coger pieza
        AudioManager.GetInstance().Play( AudioManager.GetInstance().grabEnamelPiece);

        // Reduce la opacidad del objeto
        canvasGroup.alpha = .6f;
        // Permite que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = false;
    }

    // Método que se llama mientras se arrastra el objeto
    public void OnDrag ( PointerEventData eventData )
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Método que se llama al finalizar el arrastre del objeto
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;

        if(eventData.pointerEnter == null) rectTransform.anchoredPosition = initialPosition;
        
        else if(eventData.pointerEnter.CompareTag("CuboEsmalte") && !isEsmaltada){ // Verifica si el objeto se soltó sobre el cubo del esmalte
            //Hacemos el cambiazo
            canvasGroup.alpha = 0;
            StartCoroutine(DelayedDeactivation());

        }
        else if(eventData.pointerEnter.CompareTag("EspacioMesa") && isEsmaltada){
            rectTransform.anchoredPosition = new Vector2(321, -103);
            botonListo.SetActive(true);
        }

        else rectTransform.anchoredPosition = initialPosition;
    }

    IEnumerator DelayedDeactivation(){

        // Reproducir sonido de pieza esmaltandose
        AudioManager.GetInstance().Play( AudioManager.GetInstance().EnamelPiece);

        yield return new WaitForSeconds(1);
        piezaEsmaltada.SetActive(true);

        // Reproducir sonido de pieza esmaltada
        AudioManager.GetInstance().Play( AudioManager.GetInstance().EnamelPiece);

        piezaEsmaltada.GetComponent<DragDropEsmalte>().enabled = true;
        gameObject.SetActive(false);
    }
}
