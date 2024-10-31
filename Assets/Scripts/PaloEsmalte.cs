using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaloEsmalte : MonoBehaviour, IBeginDragHandler , IEndDragHandler , IDragHandler
{
    RectTransform rectTransform;
    float anchoredXAxis, anchoredYAxis;
    [SerializeField] float moveSpeed = 1f;
    bool isMixing = false;
    bool finishedMixing = false;
    bool isDraggable = true;
    [SerializeField] float mixingWaitTime;
    [SerializeField] GameObject logicManager;
    
    void Awake(){
        rectTransform = GetComponent<RectTransform>();

    }

    void Start(){
        rectTransform.anchoredPosition = new Vector2(-500, 200); //Posición de inicio
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
            anchoredXAxis = rectTransform.anchoredPosition.x;
            anchoredYAxis = rectTransform.anchoredPosition.y;
            if(anchoredXAxis > -650f && anchoredYAxis >= 200f){ //Cuadrante superior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeed, anchoredYAxis + moveSpeed);
            }
            else if(anchoredXAxis <= -650f && anchoredYAxis > 200f){ //Cuadrante superior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis - moveSpeed, anchoredYAxis - moveSpeed);
            }
            else if(anchoredXAxis < -650f && anchoredYAxis <= 200f){ //Cuadrante inferior izquierdo
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeed, anchoredYAxis - moveSpeed);
            }
            else if(anchoredXAxis >= -650f && anchoredYAxis < 200f){ //Cuadrante inferior derecho
                rectTransform.anchoredPosition = new Vector2(anchoredXAxis + moveSpeed, anchoredYAxis + moveSpeed);
            }
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        if(!finishedMixing){
            StopAllCoroutines();
            isMixing = false;
        }
        else{
            rectTransform.anchoredPosition = new Vector2(-500, 200);
            logicManager.GetComponent<LogicEsmalte>().FirstActivityFinished();
        } 
    }

    IEnumerator MixingCountDown(){
        yield return new WaitForSeconds(mixingWaitTime);
        finishedMixing = true;
        isDraggable = false;
        OnEndDrag(null);
    }
}
