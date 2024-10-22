using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

public class CombustibleHorno : MonoBehaviour
{

    private bool isDraggingAllowed; // Booleano que indica el permiso de arrastre
    [SerializeField] private float woodLife = 5f; // Segundos de duración del quemado de la madera
    private float currentLife; // Unidades de vida que tiene la madera mientras se quema
    private bool isBurning;
    private bool isBurnt;
    public float heatValue = 0;
    private TemperaturaHorno temperatureScript;
    [SerializeField] private int heatValueMultiplier;
    private Image imageComponent;

    void Start()
    {
        isDraggingAllowed = true;
        isBurning = false;
        currentLife = woodLife;
        isBurnt = false;
        temperatureScript = FindObjectOfType<TemperaturaHorno>();
        imageComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //La madera comienza a perder vida al ser colocada en un slot y al terminar, cambia el sprite y vuelve a permitir el arrastre
        if(isBurning)
        {   
            currentLife -= Time.deltaTime; // Pierde 1 unidad de vida por segundo
            if(currentLife <= 0)
            {
                isBurning = false;
                isBurnt = true;
                imageComponent.color = Color.red;
                ChangeDragPermission(true);
                GetComponent<DragDropMinigame4>().SetCurrentMaderitaAsNull();
            }
        }
    }

    public void StartBurning()
    {   
        //Si no es madera quemada, comienza el proceso de quemado y bloquea el arrastre
        if(!isBurnt)
        {
            isBurning = true;  
            ChangeDragPermission(false);
            //Al comenzar el quemado, la madera adquiere un valor aleatorio de vida entre 4 y 8 inclusives
            woodLife = UnityEngine.Random.Range(4, 9); //Escrito como UnityEngine porque hay conflictos entre varios métodos con el mismo nombre
            currentLife = woodLife;
            heatValue = woodLife * heatValueMultiplier; //El valor de calor que aporta una madera depende de su duración
        } 
    }

    public bool IsDraggingAllowed() //Getter para el booleano isDraggingAllowed
    {
        return isDraggingAllowed;
    }

    public void ChangeDragPermission(bool targetBool) // Función simple para cambiar los permisos de arrastre
    {   
        isDraggingAllowed = targetBool;
    }

    public bool IsBurnt()
    {
        return isBurnt;
    }

    public void ChangeBurntStatus(bool targetBool)
    {
        isBurnt = targetBool;
    }
    public void ResetMaderita()
    {
        //Si la madera no estaba consumida, no hace nada
        //Si está consumida, resetea la posición, el color y su estado de quemado
        if(isBurnt)
        {
            
            imageComponent.color = Color.white;
            isBurnt = false;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(796, 45);
        }
    }

    public void SetWoodLife(float value)
    {
        woodLife = value;
    }
}
