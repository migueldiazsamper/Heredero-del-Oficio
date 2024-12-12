using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TemperaturaHorno : MonoBehaviour
{

    [SerializeField]    TextMeshProUGUI text;
    [SerializeField]    TextMeshProUGUI scoreText;
    private float totalTemperature;
    [SerializeField]    GameObject[] itemSlotArray;
    public float coolingValue;
    [SerializeField] Image thermometer;
    [SerializeField] float maxTemp = 1500f;
    [SerializeField] GameObject ceramic1;
    SpriteRenderer ceramic1Image;
    [SerializeField] GameObject ceramic2;
    SpriteRenderer ceramic2Image;
    int score = 0;

    void Awake(){
        ceramic1Image = ceramic1.GetComponent<SpriteRenderer>();
        ceramic2Image = ceramic2.GetComponent<SpriteRenderer>();
    }

    void Start(){
        StartCoroutine(ScoreManagementCoroutine());
    }

    void Update()
    {
        if(!CountDownTimer.instance.GetisTimeUp())
        {
            CalculateTotalTemp();
            text.text = Math.Floor(totalTemperature).ToString();
            scoreText.text = score.ToString();
            if(totalTemperature <= 0) totalTemperature = 0;
            if (totalTemperature >= maxTemp) totalTemperature = maxTemp;
            thermometer.fillAmount = totalTemperature/maxTemp;
            ChangeCeramicColor();
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public void CalculateTotalTemp()
    {
        float maderitaHeatValue;
        foreach (GameObject itemSlot in itemSlotArray)
        {   
            maderitaHeatValue = itemSlot.GetComponent<ItemSlot>().CurrentMaderitaHeatValue();
            if(maderitaHeatValue >= 0) totalTemperature += maderitaHeatValue*Time.deltaTime;
            else totalTemperature -= coolingValue*Time.deltaTime;
        }
    }

    void ChangeCeramicColor(){
        if(totalTemperature < 500){
            ceramic1Image.color = Color.blue;
            ceramic2Image.color = Color.blue;
        }
        else if(totalTemperature < 1000){
            ceramic1Image.color = Color.cyan;
            ceramic2Image.color = Color.cyan;
        }
        else if(totalTemperature < 1200){
            ceramic1Image.color = Color.yellow;
            ceramic2Image.color = Color.yellow;
        }
        else if (totalTemperature <= 1400){
            ceramic1Image.color = Color.white;
            ceramic2Image.color = Color.white;
        }
        else{
            ceramic1Image.color = Color.red;
            ceramic2Image.color = Color.red;
        }
    }

    IEnumerator ScoreManagementCoroutine(){ //Cada segundo y medio se chequeará la temperatura y se cambiará la puntuación acorde a ella
        
        while(true){
            
            //Cambia la puntuación
            if(totalTemperature < 1200) {
                score -= 1;
                yield return new WaitForSeconds(1.5f); //Espera 1.5s para volver a comprobar
            }
            else if(totalTemperature <= 1400){
                score += 1;

                // Reproducir sonido feedback positivo
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().positiveFeedback);

                yield return new WaitForSeconds(1.5f); //Espera 1.5s para volver a comprobar
            } 
            else {
                score -= 1;

                // Reproducir sonido feedback negativo
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().negativeFeedback);

                yield return new WaitForSeconds(0.5f); //Si estás sobrecalentado, restas más puntos y más rápido
                
            }

            if(score < 0) score = 0;
            
        }
    }
}
