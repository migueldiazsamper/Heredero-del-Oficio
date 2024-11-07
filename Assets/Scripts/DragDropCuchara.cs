using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

public class DragDropCuchara : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Referencias a las partes que conforman la cuchara
    [SerializeField] GameObject cabeza;
    [SerializeField] GameObject cuerpo;
    [SerializeField] GameObject liquido;
    private CanvasGroup cabezaCanvasGroup;
    private CanvasGroup cuerpoCanvasGroup;
    private CanvasGroup liquidoCanvasGroup;
    //
    [SerializeField] private float offSetBoteX;
    [SerializeField] private float offSetBoteY;
    
    // Referencia al componente Canvas, asignada desde el inspector de Unity
    [ SerializeField ] private Canvas canvas;

    // Referencias a los componentes RectTransform y CanvasGroup del objeto
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isCarryingPigment = false; //Booleano que cambia a true después de sacar la cuchara de un bote de pigmento
    private Vector2 startingPosition;


    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {
        // Obtiene y almacena el componente RectTransform del objeto
        rectTransform = GetComponent< RectTransform >();
        // Obtiene y almacena el componente CanvasGroup del objeto
        canvasGroup = GetComponent< CanvasGroup >();

        //Obtenemos los CanvasGroup de las partes
        cabezaCanvasGroup =     cabeza.GetComponent< CanvasGroup >();
        cuerpoCanvasGroup =     cuerpo.GetComponent< CanvasGroup >();
        liquidoCanvasGroup =    liquido.GetComponent< CanvasGroup >();
    }

    public void Start (){
        startingPosition = rectTransform.position;
        liquidoCanvasGroup.alpha = 0f;
    }

    public void OnBeginDrag(PointerEventData pointerEventData){
        
        // Reduce la opacidad del objeto
        canvasGroup.alpha = .9f;
        // Permite que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = false;
        // Coloca el objeto en un ángulo de 45º
        rectTransform.rotation = Quaternion.Euler( 0 , 0 , 45 );

        if(isCarryingPigment){
            liquidoCanvasGroup.alpha = 1f;
            cabezaCanvasGroup.alpha = 1f;

        }
    }   
    public void OnDrag(PointerEventData pointerEventData)
    {
        // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
        rectTransform.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        // Restaura la opacidad del objeto
        canvasGroup.alpha = 1f;
        // Impide que el objeto sea atravesado por rayos de detección
        canvasGroup.blocksRaycasts = true;
        
        ///Al dejar la cuchara sobre un bote de pigmentos hacemos invisible todo menos el cuerpo
        ///y el cuerpo queda sobre el bote para dar el efecto de que la cuchara está dentro del bote
        if(pointerEventData.pointerEnter.CompareTag("BotePigmento")){
            cabezaCanvasGroup.alpha = 0;
            liquidoCanvasGroup.alpha = 0;
            rectTransform.rotation = Quaternion.Euler( 0, 0, 165);
            RectTransform boteRectTransform = pointerEventData.pointerEnter.GetComponent< RectTransform >();
            rectTransform.anchoredPosition = new Vector2(boteRectTransform.anchoredPosition.x + offSetBoteX ,boteRectTransform.anchoredPosition.y + offSetBoteY);
            isCarryingPigment = true;

            liquido.GetComponent<Image>().color = pointerEventData.pointerEnter.GetComponent<Pigmento>().ProvideColor();
        }
        
        
        else if(isCarryingPigment){ //Si lleva pigmento y no la sueltas en el bol
            if(pointerEventData.pointerEnter.CompareTag("BolCombinarPigmentos")){
                //code
            }
            isCarryingPigment = false;
            liquidoCanvasGroup.alpha = 0;
        }


        // Coloca el objeto en un ángulo de 90º
        else rectTransform.rotation = Quaternion.Euler( 0 , 0 , 45 );


        
    }
}
