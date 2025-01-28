using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorFinalMancerina : MonoBehaviour
{
    [ SerializeField ] private Image color1;
    [ SerializeField ] private Image color2;
    [ SerializeField ] private Image color3;

    private void Awake ()
    {
        color1.color = PhasesManager.instance.coloresMancerina[0];
        color2.color = PhasesManager.instance.coloresMancerina[1];
        color3.color = PhasesManager.instance.coloresMancerina[2];
    }

    public void GoToMainMenu ()
    {
        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit ()
    {
        Application.Quit();
    }

    public void playButtonSound()
    {
        // Reproducir sonido botón
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().buttonClick, AudioManager.GetInstance().buttonClickVolume);
    }
}
