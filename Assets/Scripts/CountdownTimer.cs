using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    // Referencia al componente ConstantRotation
    [SerializeField] ConstantRotation constantRotation;

    // Referencia pública al botón de victoria
    public Button victoryButton;

    [SerializeField] private int minigame;

    // Método que devuelve si el tiempo se ha agotado
    public bool GetisTimeUp()
    {
        return isTimeUp;
    }
    
    // Método que establece si el tiempo se ha agotado
    public void SetisTimeUp(bool value)
    {
        isTimeUp = value;
    }

    // Método que se llama al inicio del juego que inicializa el tiempo restante
    void Start()
    {
        remainingTime = startMinutes * 60 + startSeconds;
        UpdateTimerText();
        victoryButton.gameObject.SetActive(false);
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
        if (remainingTime > 0 && !isTimeUp)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                constantRotation.MakeFinish();
                isTimeUp = true;
                UpdateTimerText();

                victoryButton.gameObject.SetActive(true);

            }
        }
        else
        {
            timerText.text = "00:00";
            if (minigame == 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}