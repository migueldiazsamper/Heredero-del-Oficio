using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Definición de la clase Timer que hereda de MonoBehaviour
public class Timer : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI para mostrar el tiempo
    [ SerializeField ] TextMeshProUGUI timerText;
    // Variable para almacenar el tiempo transcurrido
    float elapsedTime = 0f;
    
    // Método Update que se llama una vez por frame
    void Update ()
    {
        // Incrementa el tiempo transcurrido en función del tiempo entre frames
        elapsedTime += Time.deltaTime;
        // Calcula los minutos y segundos a partir del tiempo transcurrido
        int minutes = Mathf.FloorToInt( elapsedTime / 60 );
        int seconds = Mathf.FloorToInt( elapsedTime % 60 );
        // Actualiza el texto del temporizador en pantalla
        timerText.text = string.Format( "{0:00}:{1:00}" , minutes , seconds );
    }

    public float ElapsedTime(){
        return elapsedTime;
    }
}