using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckCompletionMinigame2 : MonoBehaviour
{
    [SerializeField] private GameObject[] slots; // Array de slots del minijuego
    [SerializeField] private GameObject[] pieces;// Array de piezas del minijuego
    [SerializeField] private GameObject listoButton; // Botón de listo
    public int correctRotation = -1; // Rotación correcta
    [ SerializeField ] Animator bienHechoAnimator;
    [ SerializeField ] GameObject bienHecho;

    void Start()
    {
        listoButton.SetActive(false);
        bienHecho.SetActive(false);
    }

    void Update()
    {
        if(AreAllSlotsFree()) correctRotation = -1; // Si todos los slots están libres, se reinicia la rotación correcta
        if(CheckCompletion()) {
            listoButton.SetActive(true);
            FindAnyObjectByType<Timer>().Stop();
            DisableAllPieces();
            if ( ! bienHecho.activeSelf )
            {
                bienHecho.SetActive(true);
                bienHechoAnimator.SetTrigger("BienHecho");
            }
        }
    }

    private bool AreAllSlotsFree(){ 
        foreach (var slot in slots)
        {
            if(slot.GetComponent<ItemSlot>().freeOfItem == false) return false;
        }
        return true;
    }

    private bool IsAnySlotFree(){
        foreach (var slot in slots)
        {
            if(slot.GetComponent<ItemSlot>().freeOfItem == true) return true;
        }
        return false;
    }

    private bool CheckCompletion(){
        foreach (var piece in pieces)
        {  
            // Si alguna pieza no está en la rotación que se ha decidido como correcta, se devuelve false
            if (piece.GetComponent<DraggableItem>().currentRotation != correctRotation || IsAnySlotFree()) return false; 
        }
        return true;
    }

    private void DisableAllPieces(){
        foreach (var piece in pieces){
            piece.GetComponent<DraggableItem>().enabled = false;
            piece.GetComponent<DragDropMinigame2>().enabled = false;
        }
    }
}