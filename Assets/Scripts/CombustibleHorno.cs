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
    

    // Start is called before the first frame update
    void Start()
    {
        isDraggingAllowed = true;
        isBurning = false;
        currentLife = woodLife;
        isBurnt = false;
        temperatureScript = FindObjectOfType<TemperaturaHorno>();
    }

    // Update is called once per frame
    void Update()
    {
        //La madera comienza a perder vida al ser colocada en un slot y al terminar, cambia el sprite y vuelve a permitir el arrastre
        if(isBurning)
        {   
            currentLife -= Time.deltaTime; // Pierde 1 unidad de vida por segundo
            heatValue += Time.deltaTime; // La temperatura que le da al horno aumenta en 2 por segundo
            if(currentLife <= 0)
            {
                isBurning = false;
                isBurnt = true;
                GetComponent<Image>().color = Color.red;
                ChangeDragPermission(true);
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
            temperatureScript.AddHeat(heatValue);

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
}
