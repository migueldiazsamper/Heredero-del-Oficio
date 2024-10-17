using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperaturaHorno : MonoBehaviour
{

    private float currentTemp;
    [SerializeField] TextMeshProUGUI text;
    private float totalTemperature;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //currentTemp = totalTemperature;
        totalTemperature -= Time.deltaTime*5;
        //totalTemperature = 0;
        text.text = Math.Floor(totalTemperature).ToString();
        if(totalTemperature <= 0) totalTemperature = 0;
/*         currentTemp = totalTemperature;
        currentTemp -= Time.deltaTime/2;
        //totalTemperature = 0;
        text.text = Math.Floor(currentTemp).ToString();
        if(currentTemp <= 0) currentTemp = 0; */
    }

    private IEnumerator IncreaseTemperature()
    {
        yield return new WaitForSeconds(0.5f);
        currentTemp = 0;
    }

    public void AddHeat(float heatValue)
    {
        StartCoroutine(TotalHeat(heatValue));
    }

    public IEnumerator TotalHeat(float heatValue)
    {
        yield return new WaitForSeconds(0f);
        totalTemperature += heatValue;
    }
}
