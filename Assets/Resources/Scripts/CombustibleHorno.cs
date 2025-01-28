using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Resuelve el conflicto entre Image de Unity UI y VSCode.
using Image = UnityEngine.UI.Image;

/// <summary>
/// Clase que gestiona el comportamiento de la madera utilizada como combustible en un horno.
/// Incluye su vida útil, quema y efectos relacionados.
/// </summary>

public class CombustibleHorno : MonoBehaviour
{
    // Permiso para arrastrar el objeto.
    private bool isDraggingAllowed;

    // Duración en segundos de la madera al quemarse.
    [ SerializeField ] private float woodLife = 5f;

    // Vida restante de la madera mientras se quema.
    private float currentLife;

    // Indicadores de estado de la madera.
    private bool isBurning;
    private bool isBurnt;

    // Valor de calor generado por la madera.
    public float heatValue = 0;

    // Referencia al script de temperatura del horno.
    private TemperaturaHorno temperatureScript;

    // Multiplicador que afecta al valor de calor generado.
    [ SerializeField ]
    private int heatValueMultiplier;

    // Componente Image de la madera.
    private Image imageComponent;

    /// <summary>
    /// Inicializa las variables y obtiene referencias necesarias.
    /// </summary>
    
    private void Start ()
    {
        isDraggingAllowed = true;
        isBurning = false;
        currentLife = woodLife;
        isBurnt = false;
        temperatureScript = FindObjectOfType< TemperaturaHorno >();
        imageComponent = GetComponent< Image >();
    }

    /// <summary>
    /// Actualiza el estado de la madera cada frame.
    /// Gestiona la quema y finalización del proceso.
    /// </summary>
    
    private void Update ()
    {
        // Verifica si la madera está en proceso de quema.
        bool estaQuemando = isBurning;

        if ( estaQuemando )
        {
            // Reduce la vida de la madera.
            currentLife -= Time.deltaTime;

            // Verifica si la vida de la madera se ha agotado.
            bool vidaAgotada = currentLife <= 0;

            if ( vidaAgotada )
            {
                isBurning = false;
                isBurnt = true;

                // Reproducir sonido madera quemada
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().burningWood, AudioManager.GetInstance().burningWoodVolume);

                // Cambia el color para indicar que la madera está quemada.
                imageComponent.color = Color.red;

                // Permite nuevamente el arrastre.
                ChangeDragPermission( true );

                // Resetea la referencia actual de "maderita" en el componente DragDropMinigame4.
                GetComponent< DragDropMinigame4 >().SetCurrentMaderitaAsNull();
            }
        }
    }

    /// <summary>
    /// Inicia el proceso de quema de la madera si no está quemada.
    /// </summary>
    
    public void StartBurning ()
    {
        // Verifica si la madera no está quemada.
        bool maderaNoQuemada = !isBurnt;

        if ( maderaNoQuemada )
        {
            isBurning = true;

            // Bloquea el arrastre mientras la madera se quema.
            ChangeDragPermission( false );

            // Asigna un tiempo de vida aleatorio entre 4 y 8 segundos.
            woodLife = UnityEngine.Random.Range( 4, 9 );
            currentLife = woodLife;

            // Calcula el valor de calor basado en la duración de la madera.
            heatValue = woodLife * heatValueMultiplier;
        }
    }

    /// <summary>
    /// Devuelve si el objeto se puede arrastrar.
    /// </summary>
    
    public bool IsDraggingAllowed ()
    {
        return isDraggingAllowed;
    }

    /// <summary>
    /// Cambia el estado de permiso para arrastrar el objeto.
    /// </summary>
    
    public void ChangeDragPermission ( bool targetBool )
    {
        isDraggingAllowed = targetBool;
    }

    /// <summary>
    /// Devuelve si la madera está quemada.
    /// </summary>
    
    public bool IsBurnt ()
    {
        return isBurnt;
    }

    /// <summary>
    /// Cambia el estado de quemado de la madera.
    /// </summary>
    
    public void ChangeBurntStatus ( bool targetBool )
    {
        isBurnt = targetBool;
    }

    /// <summary>
    /// Resetea el estado de la madera si está quemada.
    /// </summary>
    
    public void ResetMaderita ()
    {
        // Verifica si la madera está quemada.
        bool maderaQuemada = isBurnt;

        if ( maderaQuemada )
        {
            // Cambia el color a blanco para indicar que está limpia.
            imageComponent.color = Color.white;

            // Cambia el estado a no quemado.
            isBurnt = false;

            // Resetea la posición de la madera.
            GetComponent< RectTransform >().anchoredPosition = new Vector2( 796, 45 );
        }
    }

    /// <summary>
    /// Establece el tiempo de vida de la madera.
    /// </summary>
    
    public void SetWoodLife ( float value )
    {
        woodLife = value;
    }
}
