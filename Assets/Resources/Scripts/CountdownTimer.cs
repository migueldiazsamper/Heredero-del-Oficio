using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

// Definición de la clase Timer que hereda de MonoBehaviour
public class CountDownTimer : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI para mostrar el tiempo
    [ SerializeField ] TextMeshProUGUI timerText;
    
    // Minutos iniciales configurables
    [SerializeField] int startMinutes = 1;

    // Segundos iniciales configurables
    [SerializeField] int startSeconds = 0;

    // Variable para almacenar el tiempo restante
    private float remainingTime;

    // Variable para almacenar si el tiempo se ha agotado
    private bool isTimeUp = false;
    private bool isFinished = false;
    private bool isDefeat = false;

    // Referencia al componente ConstantRotation
    [SerializeField] ConstantRotation constantRotation;

    // Referencia pública al botón de victoria
    public Button victoryButton;

    [SerializeField] private int minigame;

    // Instancia estática para implementar el patrón Singleton
    public static CountDownTimer instance;

    // Método que devuelve si el tiempo se ha agotado
    public bool GetisDefeat()
    {
        return isDefeat;
    }

    public bool GetisTimeUp()
    {
        return isTimeUp;
    }
    
    public void Awake()
    {
        instance = this;
    }
    
    // Método que establece si el tiempo se ha agotado
    public void SetDefeat()
    {
        // Reproducir sonido derrota
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().negativeFeedback);

        isDefeat = true;
    }


    // Método que se llama al inicio del juego que inicializa el tiempo restante
    void Start()
    {
        remainingTime = startMinutes * 60 + startSeconds;
        UpdateTimerText();
        if (minigame == 3)
        {
            victoryButton.gameObject.SetActive(false);
        }
    }

    // Método que actualiza el texto del temporizador en pantalla con el tiempo restante
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Método que se llama una vez por frame que decrementa el tiempo restante
    void Update()
    {
        if (remainingTime > 0 && !isTimeUp && !isDefeat)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                if (minigame == 3)
                {
                    constantRotation.MakeFinish();
                }

                if (minigame == 4)
                {
                    // Reproducir sonido campanario
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().timeUpBell);
                }

                isTimeUp = true;
                UpdateTimerText();

                victoryButton.gameObject.SetActive(true);
            }
        }
        else
        {
            if (minigame == 3)
            {
                if(!isDefeat && !isFinished) 
                {
                    // Reproducir sonido correcto
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback);

                    isFinished = true;
                }
            }
        }
    }
}