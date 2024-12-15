using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

public class DragDropCuchara : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject listoButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] PigmentosManager pigmentosManager;

    private bool isValidColor = false;

    // Color al que se debe llegar al mezclar todos los pigmentos
    [SerializeField] private Color targetColor = PigmentosManager.colorNaranja; 
    
    //Referencias a las partes que conforman la cuchara
    [SerializeField] GameObject cabeza;
    [SerializeField] GameObject cuerpo;
    [SerializeField] GameObject liquido;
    private CanvasGroup cabezaCanvasGroup;
    private CanvasGroup cuerpoCanvasGroup;
    private CanvasGroup liquidoCanvasGroup;
    //Offsets para asegurar que la cuchara queda bien al quedar solo el cuerpo visible en el bote
    [SerializeField] private float offSetBoteX;
    [SerializeField] private float offSetBoteY;
    
    // Referencia al componente Canvas, asignada desde el inspector de Unity
    [ SerializeField ] private Canvas canvas;

    // Referencias a los componentes RectTransform y CanvasGroup del objeto
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isCarryingPigment = false; //Booleano que cambia a true después de sacar la cuchara de un bote de pigmento

    private string currentColor;

    ///Variables relacionadas con la fase de mezclar todos los pigmentos del bol
    ///Spawning position for mixing
    const int STARTING_POSITION_X = 180; 
    const int STARTING_POSITION_Y = 30; 
    
    //Half points used to calculate the sense of the movement
    const float HALF_POSITION_X = 15f;
    const float HALF_POSITION_Y = 30f;
    private bool isLockedToBowl = false;
    private bool isMixing = false;
    private float anchoredXAxis, anchoredYAxis;
    [SerializeField] float moveSpeedX = 1f;
    float moveSpeedY;
    private bool finishedMixing = false;
    [SerializeField] private float mixingWaitTime;



    // Método Awake que se llama al inicializar el script
    private void Awake ()
    {   
        moveSpeedY = moveSpeedX / 1.5f;

        pigmentosManager = FindFirstObjectByType<PigmentosManager>();
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
        liquidoCanvasGroup.alpha = 0f;
    }

    public void OnBeginDrag(PointerEventData pointerEventData){
        if (pointerEventData.button != PointerEventData.InputButton.Left) return;
        
        if(!isLockedToBowl){
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
    }   
    public void OnDrag(PointerEventData pointerEventData)
    {   
        if (pointerEventData.button != PointerEventData.InputButton.Left) return;

        if(isLockedToBowl){
            if(!isMixing){
                StartCoroutine(MixingCountDown());
                isMixing = true;
            }

            // Reproducir sonido mezclar pigmentos del bol
            if (!AudioManager.GetInstance().SFXSource.isPlaying)
            {
                AudioManager.GetInstance().Play(AudioManager.GetInstance().mixingSpoonPigments);
            }

            anchoredXAxis = rectTransform.anchoredPosition.x;
            anchoredYAxis = rectTransform.anchoredPosition.y;
            if(anchoredXAxis > HALF_POSITION_X && anchoredYAxis >= HALF_POSITION_Y){ //Cuadrante superior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeedX, anchoredYAxis + moveSpeedY);
            }
            else if(anchoredXAxis <= HALF_POSITION_X && anchoredYAxis > HALF_POSITION_Y){ //Cuadrante superior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeedX, anchoredYAxis - moveSpeedY);
            }
            else if(anchoredXAxis < HALF_POSITION_X && anchoredYAxis <= HALF_POSITION_Y){ //Cuadrante inferior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeedX, anchoredYAxis - moveSpeedY);
            }
            else if(anchoredXAxis >= HALF_POSITION_X && anchoredYAxis < HALF_POSITION_Y){ //Cuadrante inferior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeedX, anchoredYAxis + moveSpeedY);
            }
        }
        // Actualiza la posición anclada del objeto basado en el movimiento del puntero y el factor de escala del canvas
        else
        { 
            rectTransform.anchoredPosition += pointerEventData.delta / canvas.scaleFactor; 
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        
        //if (pointerEventData.button != PointerEventData.InputButton.Left) return;

        // Detener sonido mezclar pigmentos del bol
        if (AudioManager.GetInstance().SFXSource.isPlaying)
        {
            AudioManager.GetInstance().StopSFX();
        }

        if(!isLockedToBowl){
            // Restaura la opacidad del objeto
            canvasGroup.alpha = 1f;
            // Impide que el objeto sea atravesado por rayos de detección
            canvasGroup.blocksRaycasts = true;
            
            ///Al dejar la cuchara sobre un bote de pigmentos hacemos invisible todo menos el cuerpo
            ///y el cuerpo queda sobre el bote para dar el efecto de que la cuchara está dentro del bote
            if(pointerEventData.pointerEnter.CompareTag("BotePigmento") && pointerEventData != null){

                // Reproducir sonido meter cuchara en bote
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().insertSpoon);

                cabezaCanvasGroup.alpha = 0;
                liquidoCanvasGroup.alpha = 0;
                rectTransform.rotation = Quaternion.Euler( 0, 0, 165);
                RectTransform boteRectTransform = pointerEventData.pointerEnter.GetComponent< RectTransform >();
                rectTransform.anchoredPosition = new Vector2(boteRectTransform.anchoredPosition.x + offSetBoteX ,boteRectTransform.anchoredPosition.y + offSetBoteY);
                isCarryingPigment = true;

                currentColor = pointerEventData.pointerEnter.GetComponent<Pigmento>().colorString;
                liquido.GetComponent<Image>().color = pigmentosManager.ProvideColor(currentColor);
            }
            
            
            else if(isCarryingPigment){ 
                if(pointerEventData.pointerEnter.CompareTag("BolCombinarPigmentos") && pointerEventData != null){ //Si lleva pigmento y la sueltas en el bol
                    if(pigmentosManager.colorCounter < 5)
                    {
                        // Reproducir sonido meter pigmento en bol
                        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().putPigment);

                        pigmentosManager.AddColorToMix(currentColor);
                    }
                }
                isCarryingPigment = false;
                liquidoCanvasGroup.alpha = 0;
            }
            // Coloca el objeto en un ángulo de 90º
            else rectTransform.rotation = Quaternion.Euler( 0 , 0 , 45 );  
        }

        if(!finishedMixing){
            StopAllCoroutines();
            isMixing = false;
        }
        else{

            rectTransform.anchoredPosition = new Vector2(STARTING_POSITION_X, STARTING_POSITION_Y);
            pigmentosManager.MixColors();
            listoButton.SetActive(true);
            resetButton.SetActive(true);

            if(pigmentosManager.mixedColorSpriteImage.GetComponent<Image>().color == targetColor)
            {
                // Reproducir sonido feedback positivo
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback);
                isValidColor = true;
            }

            this.enabled = false;
        }

    }

    IEnumerator MixingCountDown(){
        yield return new WaitForSeconds(mixingWaitTime);
        finishedMixing = true;
        Debug.Log(finishedMixing);
        OnEndDrag(null);
    }

    public void LockToBowl(){
        // Cuando llegas al límite de colores, la cuchara se bloquea en el bol para que remuevas
        cabezaCanvasGroup.alpha = 0;
        liquidoCanvasGroup.alpha = 0;
        rectTransform.rotation = Quaternion.Euler( 0, 0, 165);
        rectTransform.anchoredPosition = new Vector2(STARTING_POSITION_X, STARTING_POSITION_Y);
        isLockedToBowl = true;
        OnEndDrag(null);
    }

    public bool IsValidColor(){
        return isValidColor;
    }
}
