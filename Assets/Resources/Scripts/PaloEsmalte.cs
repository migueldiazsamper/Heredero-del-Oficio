using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaloEsmalte : MonoBehaviour, IBeginDragHandler , IEndDragHandler , IDragHandler
{
    //Spawning position for mixing
    const int STARTING_POSITION_X = -500; 
    const int STARTING_POSITION_Y = 125; 
    
    //Half points used to calculate the sense of the movement
    const float HALF_POSITION_X = -645f;
    const float HALF_POSITION_Y = 125f;

    RectTransform rectTransform;
    float anchoredXAxis, anchoredYAxis;
    [SerializeField] float moveSpeed = 1f;
    bool isMixing = false;
    bool finishedMixing = false;
    bool isDraggable = true;
    [SerializeField] float mixingWaitTime;
    [SerializeField] GameObject logicManager;
    [SerializeField] Animator animator;
    
    void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }

    void Start(){
        rectTransform.anchoredPosition = new Vector2(STARTING_POSITION_X, STARTING_POSITION_Y); //Posición de inicio
    }

    void Update(){
        
    }

    public void OnBeginDrag(PointerEventData pointerEventData){

    }
    public void OnDrag(PointerEventData pointerEventData){
        if(isDraggable){
            if(!isMixing){
                StartCoroutine(MixingCountDown());
                isMixing = true;
            }

            animator.SetBool("isMixing", true);

            // Reproducir sonido mezclar esmalte del bol
            if (!AudioManager.GetInstance().SFXSource.isPlaying)
            {
                AudioManager.GetInstance().Play(AudioManager.GetInstance().mixingSpoonEnamel);
            }

            anchoredXAxis = rectTransform.anchoredPosition.x;
            anchoredYAxis = rectTransform.anchoredPosition.y;
            if(anchoredXAxis > HALF_POSITION_X && anchoredYAxis >= HALF_POSITION_Y){ //Cuadrante superior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeed, anchoredYAxis + moveSpeed/2);
            }
            else if(anchoredXAxis <= HALF_POSITION_X && anchoredYAxis > HALF_POSITION_Y){ //Cuadrante superior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeed, anchoredYAxis - moveSpeed/2);
            }
            else if(anchoredXAxis < HALF_POSITION_X && anchoredYAxis <= HALF_POSITION_Y){ //Cuadrante inferior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeed, anchoredYAxis - moveSpeed/2);
            }
            else if(anchoredXAxis >= HALF_POSITION_X && anchoredYAxis < HALF_POSITION_Y){ //Cuadrante inferior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeed, anchoredYAxis + moveSpeed/2);
            }
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData){

        // Detener sonido mezclar pigmentos del bol
        if (AudioManager.GetInstance().SFXSource.isPlaying)
        {
            AudioManager.GetInstance().StopSFX();
        }  

        if(!finishedMixing){
            StopAllCoroutines();
            isMixing = false;
        }
        else{
            rectTransform.anchoredPosition = new Vector2(STARTING_POSITION_X, STARTING_POSITION_Y);
            logicManager.GetComponent<LogicEsmalte>().FirstActivityFinished();
        }

        animator.SetBool("isMixing", false); 
    }

    IEnumerator MixingCountDown(){
        yield return new WaitForSeconds(mixingWaitTime);
        finishedMixing = true;
        isDraggable = false;
        OnEndDrag(null);
    }
}
