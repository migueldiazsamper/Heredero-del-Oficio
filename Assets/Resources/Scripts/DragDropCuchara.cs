using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

public class DragDropCuchara : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject listoButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject saveColorButton;
    [SerializeField] PigmentosManager pigmentosManager;

    private int numOfCorrectColors = 0;

    // Color al que se debe llegar al mezclar todos los pigmentos
    [SerializeField] private Color[][] targetColors;
    //Referencias a las partes que conforman la cuchara
    [SerializeField] GameObject cabeza;
    [SerializeField] GameObject cuerpo;
    [SerializeField] GameObject liquido;
    [SerializeField] GameObject reflejo;
    private CanvasGroup cabezaCanvasGroup;
    private CanvasGroup cuerpoCanvasGroup;
    private CanvasGroup liquidoCanvasGroup;
    private CanvasGroup reflejoCanvasGroup;
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
    private Color firstColor;
    private Color secondColor;
    private Color thirdColor;
    private Color[] savedColorsToCheck;

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
        reflejoCanvasGroup =    reflejo.GetComponent< CanvasGroup >();
    }

    public void Start (){

        liquidoCanvasGroup.alpha = 0f;
        Color[] targetColorsFirst = new Color[]
        {
            new Color(235f / 255f, 169f / 255f, 48f / 255f), // Dorado
            new Color(235f / 255f, 169f / 255f, 48f / 255f), // Dorado
            new Color(235f / 255f, 169f / 255f, 48f / 255f) // Dorado
        };
        Color[] targetColorsSecond = new Color[]
        {
            new Color(68f / 255f, 86f / 255f, 168f / 255f), // Azul Oscuro
            new Color(67f / 255f, 58f / 255f, 3f / 255f), // Verde Oliva Oscuro
            new Color(197f / 255f, 95f / 255f, 33f / 255f) // Naranja
        };
        Color[] targetColorsThird = new Color[]
        {
            new Color(186f / 255f, 221f / 255f, 243f / 255f), // Azul Claro
            new Color(141f / 255f, 132f / 255f, 73f / 255f), // Verde Oliva Claro
            new Color(194f / 255f, 140f / 255f, 102f / 255f) // Marrón
        };

        targetColors = new Color[][] { targetColorsFirst, targetColorsSecond, targetColorsThird };
        
        savedColorsToCheck = new Color[] {firstColor, secondColor, thirdColor};
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
                reflejoCanvasGroup.alpha = 1f;

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
                AudioManager.GetInstance().PlayLoop(AudioManager.GetInstance().mixingSpoonPigments, AudioManager.GetInstance().mixingSpoonPigmentsVolume);
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
            AudioManager.GetInstance().StopLoop();
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
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().insertSpoon, AudioManager.GetInstance().insertSpoonVolume);

                cabezaCanvasGroup.alpha = 0;
                liquidoCanvasGroup.alpha = 0;
                reflejoCanvasGroup.alpha = 0;
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
                        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().putPigment, AudioManager.GetInstance().putPigmentVolume);

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
            
            
            bool isColorCorrect = Array.Exists(targetColors[PhasesManager.instance.savedColors], element => element == pigmentosManager.mixedColorSpriteImage.GetComponent<Image>().color);
            //pigmentosManager.mixedColorSpriteImage.GetComponent<Image>().color == Array.Exists(targetColors[PhasesManager.instance.savedColors]);
            if(isColorCorrect)
            {
                // Reproducir sonido feedback positivo
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback, AudioManager.GetInstance().positiveFeedbackVolume);
                numOfCorrectColors++;
                savedColorsToCheck[PhasesManager.instance.savedColors] = pigmentosManager.mixedColorSpriteImage.GetComponent<Image>().color;
                if(IsRightColorCombination()) numOfCorrectColors *= 2;
                Debug.Log("COLOR CORRECTO AMEGO");
            }

            saveColorButton.SetActive(true);
            resetButton.SetActive(true);

            this.enabled = false;
        }

    }

    private bool IsRightColorCombination(){ // Sólo comprueba el segundo y tercer color porque el primero siempre es dorado
        if(numOfCorrectColors != 3) return false; //Si los 3 colores no son correctos, no se comprueba si es una combinación correcta.
        else if(Array.IndexOf(targetColors[1], secondColor) == Array.IndexOf(targetColors[2], thirdColor))
            return true;
        else return false;
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
        reflejoCanvasGroup.alpha = 0;
        rectTransform.rotation = Quaternion.Euler( 0, 0, 165);
        rectTransform.anchoredPosition = new Vector2(STARTING_POSITION_X, STARTING_POSITION_Y);
        isLockedToBowl = true;
        OnEndDrag(null);
    }

    public int NumOfCorrectColors(){
        return numOfCorrectColors;
    }

}
